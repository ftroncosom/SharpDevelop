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
using ICSharpCode.Reports.Expressions.ReportingLanguage;
using NUnit.Framework;

namespace ICSharpCode.Reports.Core.Test.ReportingLanguage.IntegrationTests
{
	[TestFixture]
	public class SubstringFixture
	{
		
		private IExpressionEvaluatorFacade evaluator;
		
		[Test]
		public void Misspelled_Function_Returns_Expression()
		{
			string expression = "=Subxstring('Sharp',5)";
			Assert.That(this.evaluator.Evaluate(expression), Is.EqualTo("Subxstring"));
		}
		
		
		[Test]
		public void Wrong_Arg_Count_Return_First_Argument()	
		{
			string expression = "=Substring('Sharp')";
			Assert.That(this.evaluator.Evaluate(expression), Is.EqualTo("Sharp"));
		}
		
		
		[Test]
		public void Substring_From_Start_To_End_Of_String()	
		{
			string expression = "=Substring('Sharp',3)";
			Assert.That(this.evaluator.Evaluate(expression), Is.EqualTo("rp"));
		}
		
		
		[Test]
		public void Substring_From_Start_To_Len()	
		{
			string expression = "=Substring('Sharp',0,3)";
			Assert.That(this.evaluator.Evaluate(expression), Is.EqualTo("Sha"));
		}
		
		
		[Test]
		public void Substring_Result_Has_Correct_Length()	
		{
			string expression = "=Substring('Sharp',0,3)";
			string val = this.evaluator.Evaluate(expression);
			Assert.That(val.Length, Is.EqualTo(3));
		}
		
		
		[Test]
		public void Concat_Substring_With_SubString()
		{
			string expression = "=Substring('SharpDevelop',0,5) + '-' + Substring('SharpDevelop',5)";
			Assert.That(this.evaluator.Evaluate(expression), Is.EqualTo("Sharp-Develop"));
		}
		

		
		[Test]
		public void Too_Much_Args_returns_Err_Message()
		{
			string expression = "=Substring('Sharp',0,3,5)";
			Assert.That(this.evaluator.Evaluate(expression),Is.StringStarting("Wrong number"));
		}
		
		
		
		[TestFixtureSetUp]
		public void Init()
		{
			this.evaluator = new ExpressionEvaluatorFacade(null);
		}
		
		[TestFixtureTearDown]
		public void Dispose()
		{
			// TODO: Add tear down code.
		}
	}
}
