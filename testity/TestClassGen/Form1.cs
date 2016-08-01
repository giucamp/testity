﻿
//   Copyright Giuseppe Campana (giu.campana@gmail.com) 2016.
// Distributed under the Boost Software License, Version 1.0.
//    (See accompanying file LICENSE_1_0.txt or copy at
//          http://www.boost.org/LICENSE_1_0.txt)

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TestClassGen
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            StringBuilder code = new StringBuilder();
            StringBuilder test = new StringBuilder();

            for( int i1 = 0; i1 < 3; i1++ )
            {
                for (int i2 = 0; i2 < 3; i2++)
                {
                    for (int i3 = 0; i3 < 3; i3++)
                    {
                        BuilClass(code, test, KindFromInt(i1), KindFromInt(i2), KindFromInt(i3), false);
                        BuilClass(code, test, KindFromInt(i1), KindFromInt(i2), KindFromInt(i3), true);
                    }
                }
            }

            var str_code = code.ToString().Replace("\r\n", "\r\n\t");

            //BuilClass(code, test, Kind.Supported, Kind.Supported, Kind.Supported);

            textBox1.Text = str_code;
            textBox2.Text = test.ToString();
        }

        enum Kind
        {
            Deleted,
            Supported,
            SupportedNoExcept
        }

        Kind KindFromInt(int i)
        {
            switch(i)
            {
                case 0: return Kind.Deleted;
                case 1: return Kind.Supported;
                case 2: return Kind.SupportedNoExcept;
                default: throw new Exception();
            }
        }

        string KindToStr(Kind i_kind)
        {
            return "FeatureKind::" + i_kind.ToString();
        }

        private void BuildTest(StringBuilder i_test, string i_trait, bool i_value)
        {
            i_test.AppendLine("static_assert( std::" + i_trait + "<ThisType>::value == " + i_value.ToString().ToLowerInvariant() + ", \""
                + i_trait + " should be " + i_value.ToString().ToLowerInvariant() + "\");");
        }

        private void BuilClass(StringBuilder i_out, StringBuilder i_test, Kind i_defaultConstructor, Kind i_copy, Kind i_move, bool i_polymorphic)
        {
            i_out.AppendLine("template <size_t SIZE, size_t ALIGNMENT>");
            i_out.AppendLine("\tclass alignas(ALIGNMENT) TestClass<" +
                KindToStr(i_defaultConstructor) + ", " +
                KindToStr(i_copy) + ", " +
                KindToStr(i_move) +
                ", SIZE, ALIGNMENT, " +
                (i_polymorphic ? "Polymorphic::Yes " : "Polymorphic::No ") +
                "> : public detail::RandomStorage<SIZE>");
            i_out.AppendLine("{");
            i_out.AppendLine("public:");

            i_out.AppendLine("\t// default constructor");
            switch (i_defaultConstructor)
            {
                case Kind.Deleted:
                    i_out.AppendLine("\tTestClass() = delete;");
                    break;
                case Kind.Supported:
                    i_out.AppendLine("\tTestClass() { exception_check_point(); }");
                    break;

                case Kind.SupportedNoExcept:
                    i_out.AppendLine("\tTestClass() noexcept = default;");
                    break;
            }

            i_out.AppendLine("");
            i_out.AppendLine("\t// constructor with int seed");
            i_out.AppendLine("\tTestClass(int i_seed) : detail::RandomStorage<SIZE>((exception_check_point(), i_seed)) { }");

            i_out.AppendLine("");
            i_out.AppendLine("\t// copy");
            switch (i_copy)
            {
                case Kind.Deleted:
                    i_out.AppendLine("\tTestClass(const TestClass &) = delete;");
                    i_out.AppendLine("\tTestClass & operator = (const TestClass &) = delete;");
                    break;
                case Kind.Supported:
                    i_out.AppendLine("\tTestClass(const TestClass & i_source)");
                    i_out.AppendLine("\t\t: detail::RandomStorage<SIZE>((exception_check_point(), i_source)) { }");
                    i_out.AppendLine("\tconst TestClass & operator = (const TestClass & i_source)");
                    i_out.AppendLine("\t\t{ exception_check_point(); detail::RandomStorage<SIZE>::operator = (i_source); return *this; }");
                    break;

                case Kind.SupportedNoExcept:
                    i_out.AppendLine("\tTestClass(const TestClass &) noexcept = default;");
                    i_out.AppendLine("\tTestClass & operator = (const TestClass &) noexcept = default;");
                    break;
            }

            i_out.AppendLine("");
            i_out.AppendLine("\t// move");
            switch (i_move)
            {
                case Kind.Deleted:
                    i_out.AppendLine("\tTestClass(TestClass &&) = delete;");
                    i_out.AppendLine("\tTestClass & operator = (TestClass &&) = delete;");
                    break;
                case Kind.Supported:
                    i_out.AppendLine("\tTestClass(TestClass && i_source)");
                    i_out.AppendLine("\t\t: detail::RandomStorage<SIZE>((exception_check_point(), std::move(i_source))) { exception_check_point(); }");
                    i_out.AppendLine("\tconst TestClass & operator = (TestClass && i_source)");
                    i_out.AppendLine("\t\t{ exception_check_point(); detail::RandomStorage<SIZE>::operator = (std::move(i_source)); return *this; }");
                    break;

                case Kind.SupportedNoExcept:
                    i_out.AppendLine("\tTestClass(TestClass &&) noexcept = default;");
                    i_out.AppendLine("\tTestClass & operator = (TestClass &&) noexcept = default;");
                    break;
            }

            if(i_polymorphic)
            {
                i_out.AppendLine("");
                i_out.AppendLine("\t// virtual destructor");
                i_out.AppendLine("\tvirtual ~TestClass() noexcept = default;");
            }

            i_out.AppendLine("");
            i_out.AppendLine("\t// comparison");
            i_out.AppendLine("\tbool operator == (const TestClass & i_other) const");
            i_out.AppendLine("\t\t{ return detail::RandomStorage<SIZE>::operator == (i_other); }");

            i_out.AppendLine("\tbool operator != (const TestClass & i_other) const");
            i_out.AppendLine("\t\t{ return detail::RandomStorage<SIZE>::operator != (i_other); }");

            i_out.AppendLine("};");
            i_out.AppendLine("");


            string className = "TestClass<" + KindToStr(i_defaultConstructor) + ", " +
                KindToStr(i_copy) + ", " + KindToStr(i_move) + ", sizeof(std::max_align_t), alignof(std::max_align_t), "
                + (i_polymorphic ? "Polymorphic::Yes" : "Polymorphic::No") + " >";
            i_test.AppendLine("");
            i_test.AppendLine("// test " + className);
            i_test.AppendLine("{ using ThisType = " + className + ";");
            BuildTest(i_test, "is_default_constructible", i_defaultConstructor != Kind.Deleted);
            BuildTest(i_test, "is_nothrow_default_constructible", i_defaultConstructor == Kind.SupportedNoExcept);

            BuildTest(i_test, "is_copy_constructible", i_copy != Kind.Deleted);
            BuildTest(i_test, "is_nothrow_copy_constructible", i_copy == Kind.SupportedNoExcept);
            BuildTest(i_test, "is_copy_assignable", i_copy != Kind.Deleted);
            BuildTest(i_test, "is_nothrow_copy_assignable", i_copy == Kind.SupportedNoExcept);

            BuildTest(i_test, "is_move_constructible", i_move != Kind.Deleted);
            BuildTest(i_test, "is_nothrow_move_constructible", i_move == Kind.SupportedNoExcept);
            BuildTest(i_test, "is_move_assignable", i_move != Kind.Deleted);
            BuildTest(i_test, "is_nothrow_move_assignable", i_move == Kind.SupportedNoExcept);

            BuildTest(i_test, "is_polymorphic", i_polymorphic);

            i_test.AppendLine("(void)( ThisType(1) == ThisType(1) && ThisType(1) != ThisType(2) ); }");
        }
    }
}
