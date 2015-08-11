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

namespace BehaviorNodeUnitTest
{
	[TestFixture]
	[Category ("SelectorLoopTest")]
	internal class SelectorLoopTest : UnitTestBase_0
    {
		[Test]
		[Category ("test_selector_loop_0")]
		public void test_selector_loop_0 ()
		{
			testAgent.btsetcurrent("node_test/selector_loop_ut_0");
			testAgent.resetProperties();
			testAgent.btexec();
			
			Assert.AreEqual(0, testAgent.testVar_0);
		}
		
		[Test]
		[Category ("test_selector_loop_1")]
		public void test_selector_loop_1 ()
		{
			testAgent.btsetcurrent("node_test/selector_loop_ut_1");
			testAgent.resetProperties();
            testAgent.btexec();
            
            Assert.AreEqual(1, testAgent.testVar_0);
        }

		[Test]
		[Category ("test_selector_loop_2")]
		public void test_selector_loop_2 ()
		{
			testAgent.btsetcurrent("node_test/selector_loop_ut_2");
			testAgent.resetProperties();
			testAgent.btexec();
			
			Assert.AreEqual(0, testAgent.testVar_0);
        }

		[Test]
		[Category ("test_selector_loop_3")]
		public void test_selector_loop_3 ()
		{
			testAgent.btsetcurrent("node_test/selector_loop_ut_3");
			testAgent.resetProperties();
			testAgent.btexec();
			
            Assert.AreEqual(-1, testAgent.testVar_0);
        }

		[Test]
		[Category ("test_selector_loop_4")]
		public void test_selector_loop_4 ()
		{
			testAgent.btsetcurrent("node_test/selector_loop_ut_4");
			testAgent.resetProperties();
			testAgent.btexec();
			Assert.AreEqual(1, testAgent.testVar_0);
			Assert.AreEqual(0, testAgent.testVar_1);

			testAgent.resetProperties();
			testAgent.btexec();
			Assert.AreEqual(1, testAgent.testVar_0);
			Assert.AreEqual(0, testAgent.testVar_1);
        }


        [Test]
        [Category("test_selector_loop_5")]
        public void test_selector_loop_5()
        {
            testAgent.btsetcurrent("node_test/selector_loop_ut_5");
            testAgent.resetProperties();
            behaviac.EBTStatus s = testAgent.btexec();
            Assert.AreEqual(behaviac.EBTStatus.BT_SUCCESS, s);
            Assert.AreEqual(1, testAgent.testVar_0);
        }
    }
    
    [TestFixture]
    [Category ("SelectorTests")]
	internal class SelectorTest : UnitTestBase_0
	{
		[Test]
		[Category ("test_selector_0")]
		public void test_selector_0 ()
		{
			testAgent.btsetcurrent("node_test/selector_ut_0");
			testAgent.resetProperties();
			testAgent.btexec();
			
			//< check int value
            Assert.AreEqual(0, testAgent.testVar_0);
        }

		[Test]
		[Category ("test_selector_1")]
		public void test_selector_1 ()
		{
			testAgent.btsetcurrent("node_test/selector_ut_1");
			testAgent.resetProperties();
			testAgent.btexec();
			
			//< check int value
            Assert.AreEqual(1, testAgent.testVar_0);
        }

		[Test]
		[Category ("test_selector_2")]
		public void test_selector_2 ()
		{
			testAgent.btsetcurrent("node_test/selector_ut_2");
			testAgent.resetProperties();
			testAgent.btexec();
			
			//< check int value
            Assert.AreEqual(2, testAgent.testVar_0);
        }

		[Test]
		[Category ("test_selector_3")]
		public void test_selector_3 ()
		{
			testAgent.btsetcurrent("node_test/selector_ut_3");
			testAgent.resetProperties();
			testAgent.btexec();
			
			//< check int value
            Assert.AreEqual(2, testAgent.testVar_0);
        }

		[Test]
		[Category ("test_selector_4")]
		public void test_selector_4 ()
		{
			testAgent.btsetcurrent("node_test/selector_ut_4");
			testAgent.resetProperties();
			testAgent.btexec();
			
			//< check int value
            Assert.AreEqual(0, testAgent.testVar_0);
        }
    }
    
    [TestFixture]
	[Category ("SequenceTests")]
	internal class SequenceTests : UnitTestBase_0
	{
		[Test]
		[Category ("test_sequence_0")]
		public void test_sequence_0 ()
		{
			testAgent.btsetcurrent("node_test/sequence_ut_0");
			testAgent.resetProperties();
			testAgent.btexec();
			
			//< check int value
			Assert.AreEqual(0, testAgent.testVar_0);
		}
		
		[Test]
		[Category ("test_sequence_1")]
		public void test_sequence_1 ()
		{
			testAgent.btsetcurrent("node_test/sequence_ut_1");
			testAgent.resetProperties();
			testAgent.btexec();
			
			//< check int value
			Assert.AreEqual(1, testAgent.testVar_0);
		}
		
		[Test]
		[Category ("test_sequence_2")]
		public void test_sequence_2 ()
		{
			testAgent.btsetcurrent("node_test/sequence_ut_2");
			testAgent.resetProperties();
			testAgent.btexec();
			
			//< check int value
			Assert.AreEqual(2, testAgent.testVar_0);
		}
		
		[Test]
		[Category ("test_sequence_3")]
		public void test_sequence_3 ()
		{
			testAgent.btsetcurrent("node_test/sequence_ut_3");
			testAgent.resetProperties();
			testAgent.btexec();
			
			//< check int value
			Assert.AreEqual(0, testAgent.testVar_0);
		}
	}

	// ---------------- IfElseTests ------------------
	[TestFixture]
	[Category ("IfElseTests")]
	internal class IfElseTests : UnitTestBase_0
	{
		[Test]
		[Category ("test_true")]
		public void test_true ()
		{
			testAgent.btsetcurrent("node_test/if_else_ut_0");
			testAgent.resetProperties();
			testAgent.btexec();
			
			//< check int value
			Assert.AreEqual(1, testAgent.testVar_0);
		}

		[Test]
		[Category ("test_false")]
		public void test_false ()
		{
			testAgent.btsetcurrent("node_test/if_else_ut_1");
			testAgent.resetProperties();
			testAgent.btexec();
			
			//< check int value
			Assert.AreEqual(2, testAgent.testVar_0);
		}
	}

	[TestFixture]
	[Category ("SequenceStochasticTests")]
	internal class SequenceStochasticTests : UnitTestBase_0
	{
		[Test]
		[Category ("test_sequence_stochastic_0")]
		public void test_sequence_stochastic_0 ()
		{
			testAgent.resetProperties();

			int[] counts = new int[]{0, 0, 0};
			int loopCount = 30000;
			while(loopCount > 0)
			{
				testAgent.btsetcurrent("node_test/sequence_stochastic_ut_0");
				testAgent.btexec();
				++(counts[testAgent.testVar_0]);
				--loopCount;
			}
			
			//< check int value
			for(int i = 0; i < counts.Length; ++i)
			{
				int k = counts[i];
				int bias = Mathf.Abs(k - 10000);
				Assert.Less(bias, 1000);
			}
		}

		void test_sequence_stochastic_distribution(string bt)
		{
			int predicateValueCount = 0;
			int loopCount = 30000;
			while(loopCount > 0)
			{
				testAgent.btsetcurrent(bt);
				testAgent.resetProperties();
				testAgent.btexec();
				if(testAgent.testVar_0 == 0)
					predicateValueCount++;
				--loopCount;
			}
			
			int bias = Mathf.Abs(predicateValueCount - 10000);
			Assert.Less(bias, 1000);
		}

		[Test]
		[Category ("test_sequence_stochastic_1")]
		public void test_sequence_stochastic_1 ()
		{
			test_sequence_stochastic_distribution("node_test/sequence_stochastic_ut_1");
		}

		[Test]
		[Category ("test_sequence_stochastic_2")]
		public void test_sequence_stochastic_2 ()
		{
			test_sequence_stochastic_distribution("node_test/sequence_stochastic_ut_2");
		}

		[Test]
		[Category ("test_sequence_stochastic_3")]
		public void test_sequence_stochastic_3 ()
		{
			test_sequence_stochastic_distribution("node_test/sequence_stochastic_ut_3");
		}
	}

	[TestFixture]
	[Category ("SelectorStochasticTests")]
	internal class SelectorStochasticTests : UnitTestBase_0
	{
        [Test]
        [Category ("test_selector_stochastic_0")]
        public void test_selector_stochastic_0 ()
        {
            testAgent.resetProperties();
            
            int[] counts = new int[]{0, 0, 0};
            int loopCount = 30000;
            while(loopCount > 0)
            {
                testAgent.btsetcurrent("node_test/selector_stochastic_ut_0");
                testAgent.btexec();
                ++(counts[testAgent.testVar_0]);
                --loopCount;
            }
            
            //< check int value
            for(int i = 0; i < counts.Length; ++i)
            {
                int k = counts[i];
                int bias = Mathf.Abs(k - 10000);
                Assert.Less(bias, 1000);
            }
        }      

        [Test]
        [Category ("test_selector_stochastic_1")]
        public void test_selector_stochastic_1 ()
        {
            int predicateValueCount = 0;
            int loopCount = 30000;
            while(loopCount > 0)
            {
                testAgent.btsetcurrent("node_test/selector_stochastic_ut_1");
                testAgent.resetProperties();
                testAgent.btexec();
                if(testAgent.testVar_0 == 0)
                    predicateValueCount++;
                --loopCount;
            }
            
            int bias = Mathf.Abs(predicateValueCount - 10000);
            Assert.Less(bias, 1000);
        }

        [Test]
        [Category ("test_selector_stochastic_2")]
        public void test_selector_stochastic_2 ()
        {
            int predicateValueCount = 0;
            int loopCount = 30000;
            while(loopCount > 0)
            {
                testAgent.btsetcurrent("node_test/selector_stochastic_ut_2");
                testAgent.resetProperties();
                testAgent.btexec();
                if(testAgent.testVar_0 == 0)
                    predicateValueCount++;
                --loopCount;
            }
            
            int bias = Mathf.Abs(predicateValueCount - 15000);
            Assert.Less(bias, 1000);
        }
	}

    [TestFixture]
    [Category ("SelectorProbabilityTests")]
    internal class SelectorProbabilityTests : UnitTestBase_0
    {
        int[] test_selector_probability_distribution(string bt)
        {
            testAgent.resetProperties();
            
            int[] counts = new int[]{0, 0, 0};
            int loopCount = 10000;
            while(loopCount > 0)
            {
                testAgent.btsetcurrent(bt);
                testAgent.btexec();
                ++(counts[testAgent.testVar_0]);
                --loopCount;
            } 

            return counts;
        }

        [Test]
        [Category ("test_selector_probability_0")]
        public void test_selector_probability_0 ()
        {
            int[] counts = test_selector_probability_distribution("node_test/selector_probability_ut_0");
            int[] targetCounts = new int[]{2000, 3000, 5000};
            for(int i = 0; i < counts.Length; ++i)
            {
                int k = counts[i];
                int bias = Mathf.Abs(k - targetCounts[i]);
                Assert.Less(bias, 1000);
            }
        }

		[Test]
		[Category ("test_selector_probability_1")]
		public void test_selector_probability_1 ()
		{
			int[] counts = test_selector_probability_distribution("node_test/selector_probability_ut_1");
			int[] targetCounts = new int[]{0, 5000, 5000};
			for(int i = 0; i < counts.Length; ++i)
			{
				int k = counts[i];
				int bias = Mathf.Abs(k - targetCounts[i]);
				Assert.Less(bias, 1000);
			}
		}

		[Test]
		[Category ("test_selector_probability_2")]
		public void test_selector_probability_2 ()
		{
			testAgent.resetProperties();

			int loopCount = 10000;
			while(loopCount > 0)
			{
				testAgent.btsetcurrent("node_test/selector_probability_ut_2");
				testAgent.btexec();
				Assert.AreEqual(testAgent.testVar_0, -1);
				--loopCount;
			}
		}
    }

	[TestFixture]
	[Category ("ConditionNodesTests")]
	internal class ConditionNodesTests : UnitTestBase_0
	{
		[Test]
		[Category ("test_condition_0")]
		public void test_condition_0 ()
		{
			testAgent.btsetcurrent("node_test/condition_ut_0");
			testAgent.resetProperties();
			testAgent.btexec();
			
            Assert.AreEqual(2, testAgent.testVar_0);
        }

		[Test]
		[Category ("test_condition_1")]
		public void test_condition_1 ()
		{
			testAgent.btsetcurrent("node_test/condition_ut_1");
			testAgent.resetProperties();
			testAgent.btexec();
			
			Assert.AreEqual(0, testAgent.testVar_0);
        }

		[Test]
		[Category ("test_condition_2")]
		public void test_condition_2 ()
		{
			testAgent.btsetcurrent("node_test/condition_ut_2");
			testAgent.resetProperties();
			testAgent.btexec();
			
            Assert.AreEqual(0, testAgent.testVar_0);
        }

		[Test]
		[Category ("test_condition_3")]
		public void test_condition_3 ()
		{
			testAgent.btsetcurrent("node_test/condition_ut_3");
			testAgent.resetProperties();
			testAgent.btexec();

			Assert.AreEqual(2, testAgent.testVar_0);
        }
    }

	[TestFixture]
	[Category ("ActionNodesTests")]
	internal class ActionNodesTests : UnitTestBase_0
	{
		[Test]
		[Category ("test_action_0")]
		public void test_action_0 ()
		{
			testAgent.btsetcurrent("node_test/action_ut_0");
			testAgent.resetProperties();
			testAgent.btexec();
			
			Assert.AreEqual(1500, testAgent.testVar_0);
			Assert.AreEqual(1800, testAgent.testVar_1);
			Assert.AreEqual(2, StaticAgent.sInt);
		}

		[Test]
		[Category ("test_action_1")]
		public void test_action_1 ()
		{
			testAgent.btsetcurrent("node_test/action_ut_1");
			testAgent.resetProperties();
			testAgent.btexec();

			Assert.AreEqual(1.8f, testAgent.testVar_2);
            Assert.AreEqual(4.5f, testAgent.testVar_3);
			Assert.AreEqual(true, "HC" == testAgent.testVar_str_0);
        }

		[Test]
		[Category ("test_action_2")]
		public void test_action_2 ()
		{
			testAgent.btsetcurrent("node_test/action_ut_2");
			testAgent.resetProperties();
			testAgent.btexec();
			
			Assert.AreEqual(500000, testAgent.testVar_0);
			Assert.AreEqual(1666, testAgent.testVar_1);
		}
		
		[Test]
		[Category ("test_action_3")]
		public void test_action_3 ()
		{
			testAgent.btsetcurrent("node_test/action_ut_3");
			testAgent.resetProperties();
			testAgent.btexec();
			
			Assert.AreEqual(2.4f, testAgent.testVar_2);
			Assert.AreEqual(4.0f, testAgent.testVar_3);
		}

		[Test]
		[Category ("test_action_waitforsignal_0")]
		public void test_action_waitforsignal_0 ()
		{
			testAgent.btsetcurrent("node_test/action_ut_waitforsignal_0");
			testAgent.resetProperties();
			testAgent.btexec();
			
			Assert.AreEqual(-1, testAgent.testVar_1);
			Assert.AreEqual(-1.0f, testAgent.testVar_2);

			testAgent.resetProperties();
			testAgent.testVar_0 = 0;
			testAgent.btexec();
			Assert.AreEqual(1, testAgent.testVar_1);
			Assert.AreEqual(2.3f, testAgent.testVar_2);
        }

		[Test]
		[Category ("test_action_waitforsignal_1")]
		public void test_action_waitforsignal_1 ()
		{
			testAgent.btsetcurrent("node_test/action_ut_waitforsignal_1");
			testAgent.resetProperties();
			testAgent.btexec();
			
			Assert.AreEqual(-1, testAgent.testVar_1);
			Assert.AreEqual(-1.0f, testAgent.testVar_2);
			
			testAgent.resetProperties();
			testAgent.testVar_2 = 0.0f;
			testAgent.btexec();
			Assert.AreEqual(1, testAgent.testVar_1);
			Assert.AreEqual(2.3f, testAgent.testVar_2);
		}

		[Test]
		[Category ("test_action_waitforsignal_2")]
		public void test_action_waitforsignal_2 ()
		{
			testAgent.btsetcurrent("node_test/action_ut_waitforsignal_2");
			testAgent.resetProperties();
			behaviac.EBTStatus status = testAgent.btexec();

			Assert.AreEqual(-1.0f, testAgent.testVar_2);
			Assert.AreEqual(behaviac.EBTStatus.BT_RUNNING, status);

			testAgent.resetProperties();
			testAgent.testVar_2 = 0.0f;
			status = testAgent.btexec();
            Assert.AreEqual(2.3f, testAgent.testVar_2);
			Assert.AreEqual(behaviac.EBTStatus.BT_SUCCESS, status);
        }

		[Test]
		[Category ("test_action_waitframes_0")]
		public void test_action_waitframes_0 ()
		{
			testAgent.btsetcurrent("node_test/action_waitframes_ut_0");
			testAgent.resetProperties();

			int loopCount = 0;
			while(loopCount < 5)
			{
				testAgent.btexec();
				if(loopCount < 4)
					Assert.AreEqual(1, testAgent.testVar_0);
				else
					Assert.AreEqual(2, testAgent.testVar_0);
				++loopCount;
			}

			behaviac.Workspace.SetDeltaFrames(5);
			testAgent.resetProperties();
			testAgent.btexec();
			Assert.AreEqual(2, testAgent.testVar_0);
			behaviac.Workspace.SetDeltaFrames(1);
		}

		[Test]
		[Category ("test_action_noop_0")]
		public void test_action_noop_0 ()
		{
			testAgent.btsetcurrent("node_test/action_noop_ut_0");
			testAgent.resetProperties();
			behaviac.EBTStatus status = testAgent.btexec();
			
			Assert.AreEqual(behaviac.EBTStatus.BT_SUCCESS, status);
			Assert.AreEqual(2, testAgent.testVar_0);
        }
    }

	[TestFixture]
	[Category ("WaitNodesTests")]
	internal class WaitNodesTests : UnitTestBase_0
	{
		[Test]
		[Category ("test_wait_0")]
		public void test_wait_0 ()
		{
			testAgent.btsetcurrent("node_test/wait_ut_0");
			testAgent.resetProperties();
			testAgent.btexec();
			
			Assert.AreEqual(1, testAgent.testVar_0);
		}

		[Test]
		[Category ("test_wait_1")]
		public void test_wait_1 ()
		{
			testAgent.btsetcurrent("node_test/wait_ut_1");
			testAgent.resetProperties();
			testAgent.btexec();
			
			Assert.AreEqual(1, testAgent.testVar_0);
		}
	}
}