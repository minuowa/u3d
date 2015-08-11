/////////////////////////////////////////////////////////////////////////////////////////////////////////////////
// Tencent is pleased to support the open source community by making behaviac available.
//
// Copyright (C) 2015 THL A29 Limited, a Tencent company. All rights reserved.
//
// Licensed under the BSD 3-Clause License (the "License"); you may not use this file except in compliance with
// the License. You may obtain a copy of the License at http://opensource.org/licenses/BSD-3-Clause
//
// Unless required by applicable law or agreed to in writing, software distributed under the License is
// distributed on an "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and limitations under the License.
/////////////////////////////////////////////////////////////////////////////////////////////////////////////////

using System;
using System.Threading;
using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;
using TNS.NE.NAT;
using TNS.ST.PER.WRK;
using behaviac;

namespace ParUnitTest
{
	[TestFixture]
	[Category ("ParTests")]
	internal class ParTests
	{
		EmployeeParTestAgent parTestAgent = null;
		ParTestRegNameAgent regNameAgent = null;

		[TestFixtureSetUp]  
		public void initGlobalTestEnv()  
		{
			BehaviacSystem.Instance.init();

			GameObject registerNameObject = new GameObject();
			registerNameObject.name = "@RegisterNameAgent";
			registerNameObject.transform.localPosition = Vector3.zero;
			registerNameObject.transform.localRotation = Quaternion.identity;
			registerNameObject.transform.localScale = Vector3.one;
			regNameAgent = registerNameObject.AddComponent<ParTestRegNameAgent>();			
			regNameAgent.init();

			GameObject testAgentObject = new GameObject();
			testAgentObject.name = "@ParTestAgent";
			testAgentObject.transform.localPosition = Vector3.zero;
			testAgentObject.transform.localRotation = Quaternion.identity;
			testAgentObject.transform.localScale = Vector3.one;
			parTestAgent = testAgentObject.AddComponent<EmployeeParTestAgent>();			
			parTestAgent.init();

			UnityEngine.Debug.Log("InitTestFixture");
		}

		
		[TestFixtureTearDown]  
		public void finlGlobalTestEnv()  
		{
			parTestAgent.finl();
			regNameAgent.finl();
			BehaviacSystem.Instance.finl();
			UnityEngine.Debug.Log("FinlTestFixture");
		}

		[SetUp]  
		public void initTestEnv()  
		{
			//Debug.Log("Start A Unit Test");
		}
		
		[TearDown]  
		public void finlTestEnv()  
		{
			parTestAgent.btunloadall();
			//Debug.Log("End A Unit Test");
		}

		[Test]
		[Category ("par_test_ref_in")]
		public void par_test_ref_in()
		{
			parTestAgent.btsetcurrent("par_test/par_as_ref_param");
			parTestAgent.resetProperties();
			parTestAgent.btexec();

			// base class 0 test
			eColor ecolor_0 = (eColor)parTestAgent.GetVariable("par0_ecolor_0");
			Assert.AreEqual(eColor.BLUE, ecolor_0);

			Boolean boolean_0 = (Boolean)parTestAgent.GetVariable("par0_boolean_0");
			Assert.AreEqual(true, boolean_0);

			Char char_0 = (Char)parTestAgent.GetVariable("par0_char_0");
			Assert.AreEqual('X', char_0);

			Byte byte_0 = (Byte)parTestAgent.GetVariable("par0_byte_0");
			Assert.AreEqual(2, byte_0);

			List<Byte> byte_list_0 = (List<Byte>)parTestAgent.GetVariable("par0_byte_list_0");
			Assert.AreEqual(1, byte_list_0.Count);
			Assert.AreEqual(8, byte_list_0[0]);

			SByte sbyte_0 = (SByte)parTestAgent.GetVariable("par0_sbyte_0");
			Assert.AreEqual(-2, sbyte_0);

			List<eColor> ecolor_list_0 = (List<eColor>)parTestAgent.GetVariable("par0_ecolor_list_0");
			Assert.AreEqual(1, ecolor_list_0.Count);
			Assert.AreEqual(eColor.RED, ecolor_list_0[0]);

			List<Boolean> boolean_list_0 = (List<Boolean>)parTestAgent.GetVariable("par0_boolean_list_0");
			Assert.AreEqual(1, boolean_list_0.Count);
			Assert.AreEqual(true, boolean_list_0[0]);

			List<Char> char_list_0 = (List<Char>)parTestAgent.GetVariable("par0_char_list_0");
			Assert.AreEqual(1, char_list_0.Count);
			Assert.AreEqual('k', char_list_0[0]);

			List<SByte> sbyte_list_0 = (List<SByte>)parTestAgent.GetVariable("par0_sbyte_list_0");
			Assert.AreEqual(1, sbyte_list_0.Count);
			Assert.AreEqual(-4, sbyte_list_0[0]);

			// base class 1 test
			Int16 int16_0 = (Int16)parTestAgent.GetVariable("par1_int16_0");
			Assert.AreEqual(1, int16_0);

			Int32 int32_0 = (Int32)parTestAgent.GetVariable("par1_int32_0");
			Assert.AreEqual(2, int32_0);

			Int64 int64_0 = (Int64)parTestAgent.GetVariable("par1_int64_0");
			Assert.AreEqual(3L, int64_0);

			UInt16 uint16_0 = (UInt16)parTestAgent.GetVariable("par1_uint16_0");
			Assert.AreEqual(4, uint16_0);

			kEmployee kemployee_0 = (kEmployee)parTestAgent.GetVariable("par1_kemployee_0");
			Assert.AreEqual(3, kemployee_0.id);
			Assert.AreEqual("Tom", kemployee_0.name);
			Assert.AreEqual('X', kemployee_0.code);
			Assert.AreEqual(58.7f, kemployee_0.weight);
			Assert.AreEqual(true, kemployee_0.isMale);
			Assert.AreEqual(eColor.GREEN, kemployee_0.skinColor);
			Assert.AreEqual(parTestAgent, kemployee_0.boss);
			Assert.AreEqual("Honda", kemployee_0.car.brand);
			Assert.AreEqual(eColor.RED, kemployee_0.car.color);
			Assert.AreEqual(23000, kemployee_0.car.price);

			List<Int32> int32_list_0 = (List<Int32>)parTestAgent.GetVariable("par1_int32_list_0");
			Assert.AreEqual(1, int32_list_0.Count);
			Assert.AreEqual(5, int32_list_0[0]);

			List<kEmployee> kemployee_list_0 = (List<kEmployee>)parTestAgent.GetVariable("par1_kemployee_list_0");
			Assert.AreEqual(1, kemployee_list_0.Count);
			Assert.AreEqual(3, kemployee_list_0[0].id);
			Assert.AreEqual("Tom", kemployee_list_0[0].name);
			Assert.AreEqual('X', kemployee_list_0[0].code);
			Assert.AreEqual(58.7f, kemployee_list_0[0].weight);
			Assert.AreEqual(true, kemployee_list_0[0].isMale);
			Assert.AreEqual(eColor.GREEN, kemployee_list_0[0].skinColor);
			Assert.AreEqual(parTestAgent, kemployee_list_0[0].boss);
			Assert.AreEqual("Honda", kemployee_list_0[0].car.brand);
			Assert.AreEqual(eColor.RED, kemployee_list_0[0].car.color);
			Assert.AreEqual(23000, kemployee_list_0[0].car.price);

			// base class 2 test
			UInt32 uint32_0 = (UInt32)parTestAgent.GetVariable("par2_uint32_0");
			Assert.AreEqual(1, uint32_0);

			UInt64 uint64_0 = (UInt64)parTestAgent.GetVariable("par2_uint64_0");
			Assert.AreEqual(2, uint64_0);

			Single single_0 = (Single)parTestAgent.GetVariable("par2_single_0");
			Assert.AreEqual(3.0f, single_0);

			Double double_0 = (Double)parTestAgent.GetVariable("par2_double_0");
			Assert.AreEqual(4.0, double_0);

			String string_0 = (String)parTestAgent.GetVariable("par2_string_0");
			Assert.AreEqual("This is a string ref in test!", string_0);

			Agent agent_0 = (Agent)parTestAgent.GetVariable("par2_agent_0");
			Assert.AreEqual(parTestAgent, agent_0);

			List<Single> single_list_0 = (List<Single>)parTestAgent.GetVariable("par2_single_list_0");
			Assert.AreEqual(1, single_list_0.Count);
			Assert.AreEqual(1.0f, single_list_0[0]);

			List<String> string_list_0 = (List<String>)parTestAgent.GetVariable("par2_string_list_0");
			Assert.AreEqual(1, string_list_0.Count);
			Assert.AreEqual("This is a string ref in test!", string_list_0[0]);

			List<Agent> agent_list_0 = (List<Agent>)parTestAgent.GetVariable("par2_agent_list_0");
			Assert.AreEqual(1, agent_list_0.Count);
			Assert.AreEqual(parTestAgent, agent_list_0[0]);
		}

		[Test]
		[Category ("par_test_param_in_return")]
		public void par_test_param_in_return()
		{
			parTestAgent.btsetcurrent("par_test/par_as_left_value_and_param");
			parTestAgent.resetProperties();
			parTestAgent.btexec();

			// base class 0 test
			eColor ecolor_0 = (eColor)parTestAgent.GetVariable("par0_ecolor_0");
			Assert.AreEqual(eColor.RED, ecolor_0);
			
			Boolean boolean_0 = (Boolean)parTestAgent.GetVariable("par0_boolean_0");
			Assert.AreEqual(false, boolean_0);
			
			Char char_0 = (Char)parTestAgent.GetVariable("par0_char_0");
			Assert.AreEqual('C', char_0);
			
			Byte byte_0 = (Byte)parTestAgent.GetVariable("par0_byte_0");
			Assert.AreEqual(209, byte_0);

			List<Byte> byte_list_0 = (List<Byte>)parTestAgent.GetVariable("par0_byte_list_0");
			Assert.AreEqual(4, byte_list_0.Count);
			Assert.AreEqual(167, byte_list_0[0]);
			Assert.AreEqual(23, byte_list_0[1]);
			Assert.AreEqual(152, byte_list_0[2]);
			Assert.AreEqual(126, byte_list_0[3]);
			
			SByte sbyte_0 = (SByte)parTestAgent.GetVariable("par0_sbyte_0");
			Assert.AreEqual(-65, sbyte_0);
			
			List<eColor> ecolor_list_0 = (List<eColor>)parTestAgent.GetVariable("par0_ecolor_list_0");
			Assert.AreEqual(3, ecolor_list_0.Count);
			Assert.AreEqual(eColor.RED, ecolor_list_0[0]);
			Assert.AreEqual(eColor.GREEN, ecolor_list_0[1]);
			Assert.AreEqual(eColor.YELLOW, ecolor_list_0[2]);
			
			List<Boolean> boolean_list_0 = (List<Boolean>)parTestAgent.GetVariable("par0_boolean_list_0");
			Assert.AreEqual(3, boolean_list_0.Count);
			Assert.AreEqual(false, boolean_list_0[0]);
			Assert.AreEqual(true, boolean_list_0[1]);
			Assert.AreEqual(false, boolean_list_0[2]);
			
			List<Char> char_list_0 = (List<Char>)parTestAgent.GetVariable("par0_char_list_0");
			Assert.AreEqual(5, char_list_0.Count);
			Assert.AreEqual('d', char_list_0[0]);
			Assert.AreEqual('j', char_list_0[1]);
			Assert.AreEqual('F', char_list_0[2]);
			Assert.AreEqual('A', char_list_0[3]);
			Assert.AreEqual('m', char_list_0[4]);
			
			List<SByte> sbyte_list_0 = (List<SByte>)parTestAgent.GetVariable("par0_sbyte_list_0");
			Assert.AreEqual(4, sbyte_list_0.Count);	
			Assert.AreEqual(127, sbyte_list_0[0]);
			Assert.AreEqual(-128, sbyte_list_0[1]);
			Assert.AreEqual(0, sbyte_list_0[2]);
			Assert.AreEqual(-126, sbyte_list_0[3]);

			// base class 1 test
			Int16 int16_0 = (Int16)parTestAgent.GetVariable("par1_int16_0");
			Assert.AreEqual(328, int16_0);
			
			Int32 int32_0 = (Int32)parTestAgent.GetVariable("par1_int32_0");
			Assert.AreEqual(347, int32_0);
			
			Int64 int64_0 = (Int64)parTestAgent.GetVariable("par1_int64_0");
			Assert.AreEqual(1950L, int64_0);
			
			UInt16 uint16_0 = (UInt16)parTestAgent.GetVariable("par1_uint16_0");
			Assert.AreEqual(2551, uint16_0);
			
			kEmployee kemployee_0 = (kEmployee)parTestAgent.GetVariable("par1_kemployee_0");
			Assert.AreEqual(86, kemployee_0.id);
			Assert.AreEqual("TomJerry", kemployee_0.name);
			Assert.AreEqual('V', kemployee_0.code);
			Assert.AreEqual(117.5f, kemployee_0.weight);
			Assert.AreEqual(true, kemployee_0.isMale);
			Assert.AreEqual(eColor.BLUE, kemployee_0.skinColor);
			Assert.AreEqual(parTestAgent, kemployee_0.boss);
			Assert.AreEqual("AlphaJapan", kemployee_0.car.brand);
			Assert.AreEqual(eColor.YELLOW, kemployee_0.car.color);
			Assert.AreEqual(8700, kemployee_0.car.price);
			
			List<Int32> int32_list_0 = (List<Int32>)parTestAgent.GetVariable("par1_int32_list_0");
			Assert.AreEqual(4, int32_list_0.Count);
			Assert.AreEqual(9999, int32_list_0[0]);
			Assert.AreEqual(12345, int32_list_0[1]);
			Assert.AreEqual(0, int32_list_0[2]);
			Assert.AreEqual(235, int32_list_0[3]);
			
			List<kEmployee> kemployee_list_0 = (List<kEmployee>)parTestAgent.GetVariable("par1_kemployee_list_0");
			Assert.AreEqual(2, kemployee_list_0.Count);

			Assert.AreEqual(9, kemployee_list_0[0].id);
			Assert.AreEqual("John", kemployee_list_0[0].name);
			Assert.AreEqual('q', kemployee_list_0[0].code);
			Assert.AreEqual(110.0f, kemployee_list_0[0].weight);
			Assert.AreEqual(true, kemployee_list_0[0].isMale);
			Assert.AreEqual(eColor.GREEN, kemployee_list_0[0].skinColor);
			Assert.AreEqual(null, kemployee_list_0[0].boss);
			Assert.AreEqual("Lexus", kemployee_list_0[0].car.brand);
			Assert.AreEqual(eColor.BLUE, kemployee_list_0[0].car.color);
			Assert.AreEqual(93840, kemployee_list_0[0].car.price);

			Assert.AreEqual(4, kemployee_list_0[1].id);
			Assert.AreEqual("Jerry", kemployee_list_0[1].name);
			Assert.AreEqual('J', kemployee_list_0[1].code);
			Assert.AreEqual(60.0f, kemployee_list_0[1].weight);
			Assert.AreEqual(false, kemployee_list_0[1].isMale);
			Assert.AreEqual(eColor.WHITE, kemployee_list_0[1].skinColor);
			Assert.AreEqual(null, kemployee_list_0[1].boss);
			Assert.AreEqual("Toyota", kemployee_list_0[1].car.brand);
			Assert.AreEqual(eColor.YELLOW, kemployee_list_0[1].car.color);
			Assert.AreEqual(43000, kemployee_list_0[1].car.price);

			// base class 2 test
			UInt32 uint32_0 = (UInt32)parTestAgent.GetVariable("par2_uint32_0");
			Assert.AreEqual(63, uint32_0);
			
			UInt64 uint64_0 = (UInt64)parTestAgent.GetVariable("par2_uint64_0");
			Assert.AreEqual(389, uint64_0);
			
			Single single_0 = (Single)parTestAgent.GetVariable("par2_single_0");
			Assert.AreEqual(170.5f, single_0);
			
			Double double_0 = (Double)parTestAgent.GetVariable("par2_double_0");
			Assert.AreEqual(48.6, double_0);
			
			String string_0 = (String)parTestAgent.GetVariable("par2_string_0");
			Assert.AreEqual("originextra", string_0);
			
			Agent agent_0 = (Agent)parTestAgent.GetVariable("par2_agent_0");
			Assert.AreEqual(parTestAgent, agent_0);
			
			List<Single> single_list_0 = (List<Single>)parTestAgent.GetVariable("par2_single_list_0");
			Assert.AreEqual(3, single_list_0.Count);
			Assert.AreEqual(5.1f, single_list_0[0]);
			Assert.AreEqual(6.2f, single_list_0[1]);
			Assert.AreEqual(93.7f, single_list_0[2]);
			
			List<String> string_list_0 = (List<String>)parTestAgent.GetVariable("par2_string_list_0");
			Assert.AreEqual(5, string_list_0.Count);
			Assert.AreEqual("string0", string_list_0[0]);
			Assert.AreEqual("string1", string_list_0[1]);
			Assert.AreEqual("string2", string_list_0[2]);
			Assert.AreEqual("string3", string_list_0[3]);
			Assert.AreEqual("extra", string_list_0[4]);
			
			List<Agent> agent_list_0 = (List<Agent>)parTestAgent.GetVariable("par2_agent_list_0");
			Assert.AreEqual(3, agent_list_0.Count);
			Assert.AreEqual(null, agent_list_0[0]);
			Assert.AreEqual(null, agent_list_0[1]);
			Assert.AreEqual(parTestAgent, agent_list_0[2]);
		}

		[Test]
		[Category ("par_test_property_as_left_value")]
		public void par_test_property_as_left_value()
		{
			parTestAgent.btsetcurrent("par_test/property_as_left_value");
			parTestAgent.resetProperties();
			parTestAgent.btexec();
			
			// base class 0 test
			Assert.AreEqual(eColor.RED, parTestAgent.TV_ECOLOR_0);
			Assert.AreEqual(false, parTestAgent.TV_BOOL_0);			
			Assert.AreEqual('C', parTestAgent.TV_CHAR_0);
			Assert.AreEqual(209, parTestAgent.TV_BYTE_0);			

			List<Byte> byte_list_0 = parTestAgent.TV_LIST_BYTE_0;
			Assert.AreEqual(4, byte_list_0.Count);
			Assert.AreEqual(167, byte_list_0[0]);
			Assert.AreEqual(23, byte_list_0[1]);
			Assert.AreEqual(152, byte_list_0[2]);
			Assert.AreEqual(126, byte_list_0[3]);
			Assert.AreEqual(-65, parTestAgent.TV_SBYTE_0);
			
			Assert.AreEqual(3, parTestAgent.TV_LIST_ECOLOR_0.Count);
			Assert.AreEqual(eColor.RED, parTestAgent.TV_LIST_ECOLOR_0[0]);
			Assert.AreEqual(eColor.GREEN, parTestAgent.TV_LIST_ECOLOR_0[1]);
			Assert.AreEqual(eColor.YELLOW, parTestAgent.TV_LIST_ECOLOR_0[2]);

			Assert.AreEqual(3, parTestAgent.TV_LIST_BOOL_0.Count);
			Assert.AreEqual(false, parTestAgent.TV_LIST_BOOL_0[0]);
			Assert.AreEqual(true, parTestAgent.TV_LIST_BOOL_0[1]);
			Assert.AreEqual(false, parTestAgent.TV_LIST_BOOL_0[2]);

			Assert.AreEqual(5, parTestAgent.TV_LIST_CHAR_0.Count);
			Assert.AreEqual('d', parTestAgent.TV_LIST_CHAR_0[0]);
			Assert.AreEqual('j', parTestAgent.TV_LIST_CHAR_0[1]);
			Assert.AreEqual('F', parTestAgent.TV_LIST_CHAR_0[2]);
			Assert.AreEqual('A', parTestAgent.TV_LIST_CHAR_0[3]);
			Assert.AreEqual('m', parTestAgent.TV_LIST_CHAR_0[4]);

			List<SByte> sbyte_list_0 = parTestAgent.TV_LIST_SBYTE_0;
			Assert.AreEqual(4, sbyte_list_0.Count);	
			Assert.AreEqual(127, sbyte_list_0[0]);
			Assert.AreEqual(-128, sbyte_list_0[1]);
			Assert.AreEqual(0, sbyte_list_0[2]);
			Assert.AreEqual(-126, sbyte_list_0[3]);
			
			// base class 1 test
			Assert.AreEqual(328, parTestAgent.TV_I16_0);
			Assert.AreEqual(347, parTestAgent.TV_I32_0);
			Assert.AreEqual(1950L, parTestAgent.TV_I64_0);
			Assert.AreEqual(2551, parTestAgent.TV_UI16_0);
			
			kEmployee kemployee_0 = parTestAgent.TV_KEMPLOYEE_0;
			Assert.AreEqual(86, kemployee_0.id);
			Assert.AreEqual("TomJerry", kemployee_0.name);
			Assert.AreEqual('V', kemployee_0.code);
			Assert.AreEqual(117.5f, kemployee_0.weight);
			Assert.AreEqual(true, kemployee_0.isMale);
			Assert.AreEqual(eColor.BLUE, kemployee_0.skinColor);
			Assert.AreEqual(parTestAgent, kemployee_0.boss);
			Assert.AreEqual("AlphaJapan", kemployee_0.car.brand);
			Assert.AreEqual(eColor.YELLOW, kemployee_0.car.color);
			Assert.AreEqual(8700, kemployee_0.car.price);

			Assert.AreEqual(4, parTestAgent.TV_LIST_I32_0.Count);
			Assert.AreEqual(9999, parTestAgent.TV_LIST_I32_0[0]);
			Assert.AreEqual(12345, parTestAgent.TV_LIST_I32_0[1]);
			Assert.AreEqual(0, parTestAgent.TV_LIST_I32_0[2]);
			Assert.AreEqual(235, parTestAgent.TV_LIST_I32_0[3]);
			
			List<kEmployee> kemployee_list_0 = parTestAgent.TV_LIST_KEMPLOYEE_0;
			Assert.AreEqual(2, kemployee_list_0.Count);
			
			Assert.AreEqual(9, kemployee_list_0[0].id);
			Assert.AreEqual("John", kemployee_list_0[0].name);
			Assert.AreEqual('q', kemployee_list_0[0].code);
			Assert.AreEqual(110.0f, kemployee_list_0[0].weight);
			Assert.AreEqual(true, kemployee_list_0[0].isMale);
			Assert.AreEqual(eColor.GREEN, kemployee_list_0[0].skinColor);
			Assert.AreEqual(null, kemployee_list_0[0].boss);
			Assert.AreEqual("Lexus", kemployee_list_0[0].car.brand);
			Assert.AreEqual(eColor.BLUE, kemployee_list_0[0].car.color);
			Assert.AreEqual(93840, kemployee_list_0[0].car.price);
			
			Assert.AreEqual(4, kemployee_list_0[1].id);
			Assert.AreEqual("Jerry", kemployee_list_0[1].name);
			Assert.AreEqual('J', kemployee_list_0[1].code);
			Assert.AreEqual(60.0f, kemployee_list_0[1].weight);
			Assert.AreEqual(false, kemployee_list_0[1].isMale);
			Assert.AreEqual(eColor.WHITE, kemployee_list_0[1].skinColor);
			Assert.AreEqual(null, kemployee_list_0[1].boss);
			Assert.AreEqual("Toyota", kemployee_list_0[1].car.brand);
			Assert.AreEqual(eColor.YELLOW, kemployee_list_0[1].car.color);
			Assert.AreEqual(43000, kemployee_list_0[1].car.price);
			
			// base class 2 test
			Assert.AreEqual(63, parTestAgent.TV_UI32_0);
			Assert.AreEqual(389, parTestAgent.TV_UI64_0);
			Assert.AreEqual(170.5f, parTestAgent.TV_F_0);
			Assert.AreEqual(48.6, parTestAgent.TV_D_0);
			Assert.AreEqual("originextra", parTestAgent.TV_STR_0);
			Assert.AreEqual(parTestAgent, parTestAgent.TV_AGENT_0);
			
			List<Single> single_list_0 = parTestAgent.TV_LIST_F_0;
			Assert.AreEqual(3, single_list_0.Count);
			Assert.AreEqual(5.1f, single_list_0[0]);
			Assert.AreEqual(6.2f, single_list_0[1]);
			Assert.AreEqual(93.7f, single_list_0[2]);
			
			List<String> string_list_0 = parTestAgent.TV_LIST_STR_0;
			Assert.AreEqual(5, string_list_0.Count);
			Assert.AreEqual("string0", string_list_0[0]);
			Assert.AreEqual("string1", string_list_0[1]);
			Assert.AreEqual("string2", string_list_0[2]);
			Assert.AreEqual("string3", string_list_0[3]);
			Assert.AreEqual("extra", string_list_0[4]);
			
			List<Agent> agent_list_0 = parTestAgent.TV_LIST_AGENT_0;
			Assert.AreEqual(3, agent_list_0.Count);
			Assert.AreEqual(null, agent_list_0[0]);
			Assert.AreEqual(null, agent_list_0[1]);
			Assert.AreEqual(parTestAgent, agent_list_0[2]);
		}

		[Test]
		[Category ("property_test_ref_in")]
		public void property_test_ref_in()
		{
			parTestAgent.btsetcurrent("par_test/property_as_ref_param");
			parTestAgent.resetProperties();
			parTestAgent.btexec();
		
			// base class 0 test
			Assert.AreEqual(eColor.BLUE, parTestAgent.TV_ECOLOR_0);
			Assert.AreEqual(true, parTestAgent.TV_BOOL_0);			
			Assert.AreEqual('X', parTestAgent.TV_CHAR_0);		
			Assert.AreEqual(2, parTestAgent.TV_BYTE_0);	
			
			List<Byte> byte_list_0 = parTestAgent.TV_LIST_BYTE_0;
			Assert.AreEqual(1, byte_list_0.Count);
			Assert.AreEqual(8, byte_list_0[0]);
	
			Assert.AreEqual(-2, parTestAgent.TV_SBYTE_0);

			Assert.AreEqual(1, parTestAgent.TV_LIST_ECOLOR_0.Count);
			Assert.AreEqual(eColor.RED, parTestAgent.TV_LIST_ECOLOR_0[0]);

			Assert.AreEqual(1, parTestAgent.TV_LIST_BOOL_0.Count);
			Assert.AreEqual(true, parTestAgent.TV_LIST_BOOL_0[0]);

			Assert.AreEqual(1, parTestAgent.TV_LIST_CHAR_0.Count);
			Assert.AreEqual('k', parTestAgent.TV_LIST_CHAR_0[0]);
			
			List<SByte> sbyte_list_0 = parTestAgent.TV_LIST_SBYTE_0;
			Assert.AreEqual(1, sbyte_list_0.Count);
			Assert.AreEqual(-4, sbyte_list_0[0]);
			
			// base class 1 test
			Assert.AreEqual(1, parTestAgent.TV_I16_0);
			Assert.AreEqual(2, parTestAgent.TV_I32_0);
			Assert.AreEqual(3L, parTestAgent.TV_I64_0);
			Assert.AreEqual(4, parTestAgent.TV_UI16_0);
			
			kEmployee kemployee_0 = parTestAgent.TV_KEMPLOYEE_0;
			Assert.AreEqual(3, kemployee_0.id);
			Assert.AreEqual("Tom", kemployee_0.name);
			Assert.AreEqual('X', kemployee_0.code);
			Assert.AreEqual(58.7f, kemployee_0.weight);
			Assert.AreEqual(true, kemployee_0.isMale);
			Assert.AreEqual(eColor.GREEN, kemployee_0.skinColor);
			Assert.AreEqual(parTestAgent, kemployee_0.boss);
			Assert.AreEqual("Honda", kemployee_0.car.brand);
			Assert.AreEqual(eColor.RED, kemployee_0.car.color);
			Assert.AreEqual(23000, kemployee_0.car.price);
			
			List<Int32> int32_list_0 = parTestAgent.TV_LIST_I32_0;
			Assert.AreEqual(1, int32_list_0.Count);
			Assert.AreEqual(5, int32_list_0[0]);
			
			List<kEmployee> kemployee_list_0 = parTestAgent.TV_LIST_KEMPLOYEE_0;
			Assert.AreEqual(1, kemployee_list_0.Count);
			Assert.AreEqual(3, kemployee_list_0[0].id);
			Assert.AreEqual("Tom", kemployee_list_0[0].name);
			Assert.AreEqual('X', kemployee_list_0[0].code);
			Assert.AreEqual(58.7f, kemployee_list_0[0].weight);
			Assert.AreEqual(true, kemployee_list_0[0].isMale);
			Assert.AreEqual(eColor.GREEN, kemployee_list_0[0].skinColor);
			Assert.AreEqual(parTestAgent, kemployee_list_0[0].boss);
			Assert.AreEqual("Honda", kemployee_list_0[0].car.brand);
			Assert.AreEqual(eColor.RED, kemployee_list_0[0].car.color);
			Assert.AreEqual(23000, kemployee_list_0[0].car.price);
			
			// base class 2 test
			Assert.AreEqual(1, parTestAgent.TV_UI32_0);
			Assert.AreEqual(2, parTestAgent.TV_UI64_0);
			Assert.AreEqual(3.0f, parTestAgent.TV_F_0);
			Assert.AreEqual(4.0, parTestAgent.TV_D_0);
			Assert.AreEqual("This is a string ref in test!", parTestAgent.TV_STR_0);
			Assert.AreEqual(parTestAgent, parTestAgent.TV_AGENT_0);
			
			List<Single> single_list_0 = parTestAgent.TV_LIST_F_0;
			Assert.AreEqual(1, single_list_0.Count);
			Assert.AreEqual(1.0f, single_list_0[0]);
			
			List<String> string_list_0 = parTestAgent.TV_LIST_STR_0;
			Assert.AreEqual(1, string_list_0.Count);
			Assert.AreEqual("This is a string ref in test!", string_list_0[0]);
			
			List<Agent> agent_list_0 = parTestAgent.TV_LIST_AGENT_0;
			Assert.AreEqual(1, agent_list_0.Count);
			Assert.AreEqual(parTestAgent, agent_list_0[0]);
		}

		[Test]
		[Category ("property_test_left_value_and_param")]
		public void property_test_left_value_and_param()
		{
			parTestAgent.btsetcurrent("par_test/property_as_left_value_and_param");
			parTestAgent.resetProperties();
			parTestAgent.btexec();
			
			// base class 0 test
			Assert.AreEqual(eColor.BLUE, parTestAgent.TV_ECOLOR_0);
			Assert.AreEqual(true, parTestAgent.TV_BOOL_0);			
			Assert.AreEqual('D', parTestAgent.TV_CHAR_0);		
			Assert.AreEqual(12, parTestAgent.TV_BYTE_0);

			List<Byte> byte_list_0 = parTestAgent.TV_LIST_BYTE_0;
			Assert.AreEqual(1, byte_list_0.Count);
			Assert.AreEqual(126, byte_list_0[0]);

			Assert.AreEqual(-5, parTestAgent.TV_SBYTE_0);
			
			Assert.AreEqual(1, parTestAgent.TV_LIST_ECOLOR_0.Count);
			Assert.AreEqual(eColor.YELLOW, parTestAgent.TV_LIST_ECOLOR_0[0]);
			
			Assert.AreEqual(1, parTestAgent.TV_LIST_BOOL_0.Count);
			Assert.AreEqual(false, parTestAgent.TV_LIST_BOOL_0[0]);
			
			Assert.AreEqual(1, parTestAgent.TV_LIST_CHAR_0.Count);
			Assert.AreEqual('m', parTestAgent.TV_LIST_CHAR_0[0]);
			
			List<SByte> sbyte_list_0 = parTestAgent.TV_LIST_SBYTE_0;
			Assert.AreEqual(1, sbyte_list_0.Count);
			Assert.AreEqual(-126, sbyte_list_0[0]);
			
			// base class 1 test
			Assert.AreEqual(250, parTestAgent.TV_I16_0);
			Assert.AreEqual(350, parTestAgent.TV_I32_0);
			Assert.AreEqual(450L, parTestAgent.TV_I64_0);
			Assert.AreEqual(550, parTestAgent.TV_UI16_0);
			
			kEmployee kemployee_0 = parTestAgent.TV_KEMPLOYEE_0;
			Assert.AreEqual(2, kemployee_0.id);
			Assert.AreEqual("Jerry", kemployee_0.name);
			Assert.AreEqual('V', kemployee_0.code);
			Assert.AreEqual(20.2f, kemployee_0.weight);
			Assert.AreEqual(false, kemployee_0.isMale);
			Assert.AreEqual(eColor.BLUE, kemployee_0.skinColor);
			Assert.AreEqual(parTestAgent, kemployee_0.boss);
			Assert.AreEqual("VolkswageJapan", kemployee_0.car.brand);
			Assert.AreEqual(eColor.RED, kemployee_0.car.color);
			Assert.AreEqual(3000, kemployee_0.car.price);
			
			List<Int32> int32_list_0 = parTestAgent.TV_LIST_I32_0;
			Assert.AreEqual(1, int32_list_0.Count);
			Assert.AreEqual(235, int32_list_0[0]);
			
			List<kEmployee> kemployee_list_0 = parTestAgent.TV_LIST_KEMPLOYEE_0;
			Assert.AreEqual(4, kemployee_list_0[0].id);
			Assert.AreEqual("Jerry", kemployee_list_0[0].name);
			Assert.AreEqual('J', kemployee_list_0[0].code);
			Assert.AreEqual(60.0f, kemployee_list_0[0].weight);
			Assert.AreEqual(false, kemployee_list_0[0].isMale);
			Assert.AreEqual(eColor.WHITE, kemployee_list_0[0].skinColor);
			Assert.AreEqual(null, kemployee_list_0[0].boss);
			Assert.AreEqual("Toyota", kemployee_list_0[0].car.brand);
			Assert.AreEqual(eColor.YELLOW, kemployee_list_0[0].car.color);
			Assert.AreEqual(43000, kemployee_list_0[0].car.price);
			
			// base class 2 test
			Assert.AreEqual(54, parTestAgent.TV_UI32_0);
			Assert.AreEqual(89, parTestAgent.TV_UI64_0);
			Assert.AreEqual(72.3f, parTestAgent.TV_F_0);
			Assert.AreEqual(42.9, parTestAgent.TV_D_0);
			Assert.AreEqual("extra", parTestAgent.TV_STR_0);
			Assert.AreEqual(parTestAgent, parTestAgent.TV_AGENT_0);
			
			List<Single> single_list_0 = parTestAgent.TV_LIST_F_0;
			Assert.AreEqual(1, single_list_0.Count);
			Assert.AreEqual(93.7f, single_list_0[0]);
			
			List<String> string_list_0 = parTestAgent.TV_LIST_STR_0;
			Assert.AreEqual(1, string_list_0.Count);
			Assert.AreEqual("extra", string_list_0[0]);
			
			List<Agent> agent_list_0 = parTestAgent.TV_LIST_AGENT_0;
			Assert.AreEqual(1, agent_list_0.Count);
			Assert.AreEqual(parTestAgent, agent_list_0[0]);
		}

		[Test]
		[Category ("static_property_test_ref_in")]
		public void static_property_test_ref_in()
		{
			parTestAgent.btsetcurrent("par_test/static_property_as_ref_param");
			parTestAgent.resetProperties();
			parTestAgent.btexec();
			
			// base class 0 test
			Assert.AreEqual(eColor.BLUE, EmployeeParTestAgent.STV_ECOLOR_0);
			Assert.AreEqual(true, EmployeeParTestAgent.STV_BOOL_0);			
			Assert.AreEqual('X', EmployeeParTestAgent.STV_CHAR_0);
			
			Assert.AreEqual(1, EmployeeParTestAgent.STV_LIST_ECOLOR_0.Count);
			Assert.AreEqual(eColor.RED, EmployeeParTestAgent.STV_LIST_ECOLOR_0[0]);
			
			Assert.AreEqual(1, EmployeeParTestAgent.STV_LIST_BOOL_0.Count);
			Assert.AreEqual(true, EmployeeParTestAgent.STV_LIST_BOOL_0[0]);
			
			Assert.AreEqual(1, EmployeeParTestAgent.STV_LIST_CHAR_0.Count);
			Assert.AreEqual('k', EmployeeParTestAgent.STV_LIST_CHAR_0[0]);
			
			List<SByte> sbyte_list_0 = EmployeeParTestAgent.STV_LIST_SBYTE_0;
			Assert.AreEqual(1, sbyte_list_0.Count);
			Assert.AreEqual(-4, sbyte_list_0[0]);
			
			// base class 1 test
			Assert.AreEqual(2, EmployeeParTestAgent.STV_I32_0);

			kEmployee kemployee_0 = EmployeeParTestAgent.STV_KEMPLOYEE_0;
			Assert.AreEqual(3, kemployee_0.id);
			Assert.AreEqual("Tom", kemployee_0.name);
			Assert.AreEqual('X', kemployee_0.code);
			Assert.AreEqual(58.7f, kemployee_0.weight);
			Assert.AreEqual(true, kemployee_0.isMale);
			Assert.AreEqual(eColor.GREEN, kemployee_0.skinColor);
			Assert.AreEqual(parTestAgent, kemployee_0.boss);
			Assert.AreEqual("Honda", kemployee_0.car.brand);
			Assert.AreEqual(eColor.RED, kemployee_0.car.color);
			Assert.AreEqual(23000, kemployee_0.car.price);
			
			List<Int32> int32_list_0 = EmployeeParTestAgent.STV_LIST_I32_0;
			Assert.AreEqual(1, int32_list_0.Count);
			Assert.AreEqual(5, int32_list_0[0]);
			
			List<kEmployee> kemployee_list_0 = EmployeeParTestAgent.STV_LIST_KEMPLOYEE_0;
			Assert.AreEqual(1, kemployee_list_0.Count);
			Assert.AreEqual(3, kemployee_list_0[0].id);
			Assert.AreEqual("Tom", kemployee_list_0[0].name);
			Assert.AreEqual('X', kemployee_list_0[0].code);
			Assert.AreEqual(58.7f, kemployee_list_0[0].weight);
			Assert.AreEqual(true, kemployee_list_0[0].isMale);
			Assert.AreEqual(eColor.GREEN, kemployee_list_0[0].skinColor);
			Assert.AreEqual(parTestAgent, kemployee_list_0[0].boss);
			Assert.AreEqual("Honda", kemployee_list_0[0].car.brand);
			Assert.AreEqual(eColor.RED, kemployee_list_0[0].car.color);
			Assert.AreEqual(23000, kemployee_list_0[0].car.price);
			
			// base class 2 test
			Assert.AreEqual(3.0f, EmployeeParTestAgent.STV_F_0);
			Assert.AreEqual("This is a string ref in test!", EmployeeParTestAgent.STV_STR_0);
			Assert.AreEqual(parTestAgent, EmployeeParTestAgent.STV_AGENT_0);
            
			List<Single> single_list_0 = EmployeeParTestAgent.STV_LIST_F_0;
            Assert.AreEqual(1, single_list_0.Count);
            Assert.AreEqual(1.0f, single_list_0[0]);
            
			List<String> string_list_0 = EmployeeParTestAgent.STV_LIST_STR_0;
            Assert.AreEqual(1, string_list_0.Count);
            Assert.AreEqual("This is a string ref in test!", string_list_0[0]);
            
			List<Agent> agent_list_0 = EmployeeParTestAgent.STV_LIST_AGENT_0;
            Assert.AreEqual(1, agent_list_0.Count);
            Assert.AreEqual(parTestAgent, agent_list_0[0]);
		}

		[Test]
		[Category ("static_property_test_left_value_and_param")]
		public void static_property_test_left_value_and_param()
		{
			parTestAgent.btsetcurrent("par_test/static_property_as_left_value_and_param");
			parTestAgent.resetProperties();
			parTestAgent.btexec();
			
			// base class 0 test
			Assert.AreEqual(eColor.BLUE, EmployeeParTestAgent.STV_ECOLOR_0);
			Assert.AreEqual(true, EmployeeParTestAgent.STV_BOOL_0);			
			Assert.AreEqual('D', EmployeeParTestAgent.STV_CHAR_0);		

			Assert.AreEqual(1, EmployeeParTestAgent.STV_LIST_ECOLOR_0.Count);
			Assert.AreEqual(eColor.YELLOW, EmployeeParTestAgent.STV_LIST_ECOLOR_0[0]);
			
			Assert.AreEqual(1, EmployeeParTestAgent.STV_LIST_BOOL_0.Count);
			Assert.AreEqual(false, EmployeeParTestAgent.STV_LIST_BOOL_0[0]);
			
			Assert.AreEqual(1, EmployeeParTestAgent.STV_LIST_CHAR_0.Count);
			Assert.AreEqual('m', EmployeeParTestAgent.STV_LIST_CHAR_0[0]);

			List<SByte> sbyte_list_0 = EmployeeParTestAgent.STV_LIST_SBYTE_0;
			Assert.AreEqual(1, sbyte_list_0.Count);
			Assert.AreEqual(-126, sbyte_list_0[0]);
			
			// base class 1 test
			Assert.AreEqual(350, EmployeeParTestAgent.STV_I32_0);

			kEmployee kemployee_0 = EmployeeParTestAgent.STV_KEMPLOYEE_0;
			Assert.AreEqual(2, kemployee_0.id);
			Assert.AreEqual("Jerry", kemployee_0.name);
			Assert.AreEqual('V', kemployee_0.code);
			Assert.AreEqual(20.2f, kemployee_0.weight);
			Assert.AreEqual(false, kemployee_0.isMale);
			Assert.AreEqual(eColor.BLUE, kemployee_0.skinColor);
			Assert.AreEqual(parTestAgent, kemployee_0.boss);
			Assert.AreEqual("VolkswageJapan", kemployee_0.car.brand);
			Assert.AreEqual(eColor.RED, kemployee_0.car.color);
			Assert.AreEqual(3000, kemployee_0.car.price);
			
			List<Int32> int32_list_0 = EmployeeParTestAgent.STV_LIST_I32_0;
			Assert.AreEqual(1, int32_list_0.Count);
			Assert.AreEqual(235, int32_list_0[0]);
			
			List<kEmployee> kemployee_list_0 = EmployeeParTestAgent.STV_LIST_KEMPLOYEE_0;
			Assert.AreEqual(4, kemployee_list_0[0].id);
			Assert.AreEqual("Jerry", kemployee_list_0[0].name);
			Assert.AreEqual('J', kemployee_list_0[0].code);
			Assert.AreEqual(60.0f, kemployee_list_0[0].weight);
			Assert.AreEqual(false, kemployee_list_0[0].isMale);
			Assert.AreEqual(eColor.WHITE, kemployee_list_0[0].skinColor);
			Assert.AreEqual(null, kemployee_list_0[0].boss);
			Assert.AreEqual("Toyota", kemployee_list_0[0].car.brand);
			Assert.AreEqual(eColor.YELLOW, kemployee_list_0[0].car.color);
			Assert.AreEqual(43000, kemployee_list_0[0].car.price);
			
			// base class 2 test
			Assert.AreEqual(72.3f, EmployeeParTestAgent.STV_F_0);
			Assert.AreEqual("extra", EmployeeParTestAgent.STV_STR_0);
			Assert.AreEqual(parTestAgent, EmployeeParTestAgent.STV_AGENT_0);
			
			List<Single> single_list_0 = EmployeeParTestAgent.STV_LIST_F_0;
			Assert.AreEqual(1, single_list_0.Count);
			Assert.AreEqual(93.7f, single_list_0[0]);
			
			List<String> string_list_0 = EmployeeParTestAgent.STV_LIST_STR_0;
			Assert.AreEqual(1, string_list_0.Count);
			Assert.AreEqual("extra", string_list_0[0]);
			
			List<Agent> agent_list_0 = EmployeeParTestAgent.STV_LIST_AGENT_0;
			Assert.AreEqual(1, agent_list_0.Count);
			Assert.AreEqual(parTestAgent, agent_list_0[0]);
		}

		[Test]
		[Category ("register_name_test")]
		public void register_name_test()
		{
			parTestAgent.btsetcurrent("par_test/register_name_as_left_value_and_param");
			regNameAgent.resetProperties();
			parTestAgent.resetProperties();
			parTestAgent.btexec();

			Assert.AreEqual('D', regNameAgent.TV_CHAR_0);
			Assert.AreEqual(12, regNameAgent.TV_BYTE_0);
			Assert.AreEqual(-5, regNameAgent.TV_SBYTE_0);
			Assert.AreEqual("extra", regNameAgent.TV_STR_0);
			Assert.AreEqual(regNameAgent, regNameAgent.TV_AGENT_0);

			kEmployee kemployee_0 = regNameAgent.TV_KEMPLOYEE_0;
			Assert.AreEqual(2, kemployee_0.id);
			Assert.AreEqual("Jerry", kemployee_0.name);
			Assert.AreEqual('V', kemployee_0.code);
			Assert.AreEqual(20.2f, kemployee_0.weight);
			Assert.AreEqual(false, kemployee_0.isMale);
			Assert.AreEqual(eColor.BLUE, kemployee_0.skinColor);
			Assert.AreEqual(regNameAgent, kemployee_0.boss);
			Assert.AreEqual("VolkswageJapan", kemployee_0.car.brand);
			Assert.AreEqual(eColor.RED, kemployee_0.car.color);
			Assert.AreEqual(3000, kemployee_0.car.price);

			kEmployee kemployee_1 = ParTestRegNameAgent.STV_KEMPLOYEE_0;
			Assert.AreEqual(2, kemployee_1.id);
			Assert.AreEqual("Jerry", kemployee_1.name);
			Assert.AreEqual('V', kemployee_1.code);
			Assert.AreEqual(20.2f, kemployee_1.weight);
			Assert.AreEqual(false, kemployee_1.isMale);
			Assert.AreEqual(eColor.BLUE, kemployee_1.skinColor);
			Assert.AreEqual(regNameAgent, kemployee_1.boss);
			Assert.AreEqual("VolkswageJapan", kemployee_1.car.brand);
			Assert.AreEqual(eColor.RED, kemployee_1.car.color);
			Assert.AreEqual(3000, kemployee_1.car.price);

			List<SByte> sbyte_list_0 = ParTestRegNameAgent.STV_LIST_SBYTE_0;
			Assert.AreEqual(1, sbyte_list_0.Count);
			Assert.AreEqual(-126, sbyte_list_0[0]);

			List<kEmployee> kemployee_list_0 = regNameAgent.TV_LIST_KEMPLOYEE_0;
			Assert.AreEqual(4, kemployee_list_0[0].id);
			Assert.AreEqual("Jerry", kemployee_list_0[0].name);
			Assert.AreEqual('J', kemployee_list_0[0].code);
			Assert.AreEqual(60.0f, kemployee_list_0[0].weight);
			Assert.AreEqual(false, kemployee_list_0[0].isMale);
			Assert.AreEqual(eColor.WHITE, kemployee_list_0[0].skinColor);
			Assert.AreEqual(null, kemployee_list_0[0].boss);
			Assert.AreEqual("Toyota", kemployee_list_0[0].car.brand);
			Assert.AreEqual(eColor.YELLOW, kemployee_list_0[0].car.color);
			Assert.AreEqual(43000, kemployee_list_0[0].car.price);

			List<kEmployee> kemployee_list_1 = ParTestRegNameAgent.STV_LIST_KEMPLOYEE_0;
			Assert.AreEqual(4, kemployee_list_1[0].id);
			Assert.AreEqual("Jerry", kemployee_list_1[0].name);
			Assert.AreEqual('J', kemployee_list_1[0].code);
			Assert.AreEqual(60.0f, kemployee_list_1[0].weight);
			Assert.AreEqual(false, kemployee_list_1[0].isMale);
			Assert.AreEqual(eColor.WHITE, kemployee_list_1[0].skinColor);
			Assert.AreEqual(null, kemployee_list_1[0].boss);
			Assert.AreEqual("Toyota", kemployee_list_1[0].car.brand);
			Assert.AreEqual(eColor.YELLOW, kemployee_list_1[0].car.color);
			Assert.AreEqual(43000, kemployee_list_1[0].car.price);
		}

		[Test]
		[Category ("static_member_function_test")]
		public void static_member_function_test()
		{
			parTestAgent.btsetcurrent("par_test/static_member_function_test_0");
			parTestAgent.resetProperties();
            parTestAgent.btexec();

			Assert.AreEqual('D', parTestAgent.TV_CHAR_0);
			Assert.AreEqual(12, parTestAgent.TV_BYTE_0);
			Assert.AreEqual(-5, parTestAgent.TV_SBYTE_0);
			Assert.AreEqual("extra", parTestAgent.TV_STR_0);

			Assert.AreEqual(1, parTestAgent.TV_LIST_CHAR_0.Count);
			Assert.AreEqual('m', parTestAgent.TV_LIST_CHAR_0[0]);

			List<SByte> sbyte_list_0 = parTestAgent.TV_LIST_SBYTE_0;
			Assert.AreEqual(1, sbyte_list_0.Count);
			Assert.AreEqual(-126, sbyte_list_0[0]);

			kEmployee kemployee_0 = parTestAgent.TV_KEMPLOYEE_0;
			Assert.AreEqual(2, kemployee_0.id);
			Assert.AreEqual("Jerry", kemployee_0.name);
			Assert.AreEqual('V', kemployee_0.code);
			Assert.AreEqual(20.2f, kemployee_0.weight);
			Assert.AreEqual(false, kemployee_0.isMale);
			Assert.AreEqual(eColor.BLUE, kemployee_0.skinColor);
			Assert.AreEqual(null, kemployee_0.boss);
			Assert.AreEqual("VolkswageJapan", kemployee_0.car.brand);
			Assert.AreEqual(eColor.RED, kemployee_0.car.color);
			Assert.AreEqual(3000, kemployee_0.car.price);
			
			List<kEmployee> kemployee_list_0 = parTestAgent.TV_LIST_KEMPLOYEE_0;
			Assert.AreEqual(4, kemployee_list_0[0].id);
			Assert.AreEqual("Jerry", kemployee_list_0[0].name);
			Assert.AreEqual('J', kemployee_list_0[0].code);
			Assert.AreEqual(60.0f, kemployee_list_0[0].weight);
			Assert.AreEqual(false, kemployee_list_0[0].isMale);
			Assert.AreEqual(eColor.WHITE, kemployee_list_0[0].skinColor);
			Assert.AreEqual(null, kemployee_list_0[0].boss);
            Assert.AreEqual("Toyota", kemployee_list_0[0].car.brand);
            Assert.AreEqual(eColor.YELLOW, kemployee_list_0[0].car.color);
            Assert.AreEqual(43000, kemployee_list_0[0].car.price);

			Assert.AreEqual(89, parTestAgent.TV_UI64_0);
			Assert.AreEqual("extra", parTestAgent.TV_STR_0);

			List<String> string_list_0 = parTestAgent.TV_LIST_STR_0;
			Assert.AreEqual(1, string_list_0.Count);
			Assert.AreEqual("extra", string_list_0[0]);
			
			List<Agent> agent_list_0 = parTestAgent.TV_LIST_AGENT_0;
			Assert.AreEqual(1, agent_list_0.Count);
            Assert.AreEqual(null, agent_list_0[0]);
		}
    }
}