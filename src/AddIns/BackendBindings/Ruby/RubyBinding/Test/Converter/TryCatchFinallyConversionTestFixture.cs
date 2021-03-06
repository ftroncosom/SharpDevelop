﻿// Copyright (c) 2014 AlphaSierraPapa for the SharpDevelop Team
// 
// Permission is hereby granted, free of charge, to any person obtaining a copy of this
// software and associated documentation files (the "Software"), to deal in the Software
// without restriction, including without limitation the rights to use, copy, modify, merge,
// publish, distribute, sublicense, and/or sell copies of the Software, and to permit persons
// to whom the Software is furnished to do so, subject to the following conditions:
// 
// The above copyright notice and this permission notice shall be included in all copies or
// substantial portions of the Software.
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED,
// INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR
// PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE
// FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR
// OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER
// DEALINGS IN THE SOFTWARE.

using System;
using ICSharpCode.NRefactory;
using ICSharpCode.RubyBinding;
using NUnit.Framework;

namespace RubyBinding.Tests.Converter
{
	/// <summary>
	/// Converts a C# try-catch-finally to Ruby.
	/// </summary>
	[TestFixture]
	public class TryCatchFinallyConversionTestFixture
	{
		string csharp = "class Loader\r\n" +
						"{\r\n" +
						"    public void load(string xml)\r\n" +
						"    {\r\n" +
						"        try {\r\n" +
						"            XmlDocument doc = new XmlDocument();\r\n" +
						"            doc.LoadXml(xml);\r\n" +
						"        } catch (XmlException ex) {\r\n" +
						"            Console.WriteLine(ex.ToString());\r\n" +
						"        } finally {\r\n" +
						"            Console.WriteLine(xml);\r\n" +
						"        }\r\n" +
						"    }\r\n" +
						"}";
		
		[Test]
		public void ConvertedCode()
		{
			string expectedRuby =
				"class Loader\r\n" +
				"    def load(xml)\r\n" +
				"        begin\r\n" +
				"            doc = XmlDocument.new()\r\n" +
				"            doc.LoadXml(xml)\r\n" +
				"        rescue XmlException => ex\r\n" +
				"            Console.WriteLine(ex.ToString())\r\n" +
				"        ensure\r\n" +
				"            Console.WriteLine(xml)\r\n" +
				"        end\r\n" +
				"    end\r\n" +
				"end";
			
			NRefactoryToRubyConverter converter = new NRefactoryToRubyConverter(SupportedLanguage.CSharp);
			converter.IndentString = "    ";
			string Ruby = converter.Convert(csharp);
		
			Assert.AreEqual(expectedRuby, Ruby);
		}
	}
}
