// ---------------------------------------------------------------------
// This file is auto-generated, so please don't modify it by yourself!
// Export file: ../../Resources/data/BehaviourTreesExported/generated_behaviors.cs
// ---------------------------------------------------------------------

using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;

namespace behaviac
{
	class AgentExtra_Generated
	{
		private static Dictionary<string, FieldInfo> _fields = new Dictionary<string, FieldInfo>();
		private static Dictionary<string, PropertyInfo> _properties = new Dictionary<string, PropertyInfo>();
		private static Dictionary<string, MethodInfo> _methods = new Dictionary<string, MethodInfo>();

		public static object GetProperty(behaviac.Agent agent, string property)
		{
			Type type = agent.GetType();
			string propertyName = type.FullName + property;
			if (_fields.ContainsKey(propertyName))
			{
				return _fields[propertyName].GetValue(agent);
			}

			if (_properties.ContainsKey(propertyName))
			{
				return _properties[propertyName].GetValue(agent, null);
			}

			while (type != typeof(object))
			{
				FieldInfo field = type.GetField(property, BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Static);
				if (field != null)
				{
					_fields[propertyName] = field;
					return field.GetValue(agent);
				}

				PropertyInfo prop = type.GetProperty(property, BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Static);
				if (prop != null)
				{
					_properties[propertyName] = prop;
					return prop.GetValue(agent, null);
				}

				type = type.BaseType;
			}
			Debug.Check(false, "No property can be found!");
			return null;
		}

		public static void SetProperty(behaviac.Agent agent, string property, object value)
		{
			Type type = agent.GetType();
			string propertyName = type.FullName + property;
			if (_fields.ContainsKey(propertyName))
			{
				_fields[propertyName].SetValue(agent, value);
				return;
			}

			if (_properties.ContainsKey(propertyName))
			{
				_properties[propertyName].SetValue(agent, value, null);
				return;
			}

			while (type != typeof(object))
			{
				FieldInfo field = type.GetField(property, BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Static);
				if (field != null)
				{
					_fields[propertyName] = field;
					field.SetValue(agent, value);
					return;
				}

				PropertyInfo prop = type.GetProperty(property, BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Static);
				if (prop != null)
				{
					_properties[propertyName] = prop;
					prop.SetValue(agent, value, null);
					return;
				}

				type = type.BaseType;
			}
			Debug.Check(false, "No property can be found!");
		}

		public static object ExecuteMethod(behaviac.Agent agent, string method, object[] args)
		{
			Type type = agent.GetType();
			string methodName = type.FullName + method;
			if (_methods.ContainsKey(methodName))
			{
				return _methods[methodName].Invoke(agent, args);;
			}

			while (type != typeof(object))
			{
				MethodInfo m = type.GetMethod(method, BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Static);
				if (m != null)
				{
					_methods[methodName] = m;
					return m.Invoke(agent, args);
				}

				type = type.BaseType;
			}
			Debug.Check(false, "No method can be found!");
			return null;
		}
	}

	// Source file: NpcGoToFirst

	class DecoratorLoop_bt_NpcGoToFirst_node0 : behaviac.DecoratorLoop
	{
		public DecoratorLoop_bt_NpcGoToFirst_node0()
		{
			m_bDecorateWhenChildEnds = true;
		}
		protected override int GetCount(Agent pAgent)
		{
			return -1;
		}
	}

	class Condition_bt_NpcGoToFirst_node1 : behaviac.Condition
	{
		public Condition_bt_NpcGoToFirst_node1()
		{
		}
		public override bool enteraction_impl(Agent pAgent)
		{
			((Being)pAgent).GoToFirst();
			return true;
		}
		protected override EBTStatus update_impl(behaviac.Agent pAgent, behaviac.EBTStatus childStatus)
		{
			bool opl = (bool)((Being)pAgent).IsBeingAround();
			bool opr = true;
			bool op = opl == opr;
			return op ? EBTStatus.BT_SUCCESS : EBTStatus.BT_FAILURE;
		}
	}

	public static class bt_NpcGoToFirst
	{
		public static bool build_behavior_tree(BehaviorTree bt)
		{
			bt.SetClassNameString("BehaviorTree");
			bt.SetId(-1);
			bt.SetName("NpcGoToFirst");
#if !BEHAVIAC_RELEASE
			bt.SetAgentType("Being");
#endif
			// children
			{
				DecoratorLoop_bt_NpcGoToFirst_node0 node0 = new DecoratorLoop_bt_NpcGoToFirst_node0();
				node0.SetClassNameString("DecoratorLoop");
				node0.SetId(0);
#if !BEHAVIAC_RELEASE
				node0.SetAgentType("Being");
#endif
				bt.AddChild(node0);
				{
					Condition_bt_NpcGoToFirst_node1 node1 = new Condition_bt_NpcGoToFirst_node1();
					node1.SetClassNameString("Condition");
					node1.SetId(1);
#if !BEHAVIAC_RELEASE
					node1.SetAgentType("Being");
#endif
					node0.AddChild(node1);
					node0.SetHasEvents(node0.HasEvents() | node1.HasEvents());
				}
				bt.SetHasEvents(bt.HasEvents() | node0.HasEvents());
			}
			return true;
		}
	}

	// Source file: Npc_RandomMove_Passive

	class DecoratorLoop_bt_Npc_RandomMove_Passive_node6 : behaviac.DecoratorLoop
	{
		public DecoratorLoop_bt_Npc_RandomMove_Passive_node6()
		{
			m_bDecorateWhenChildEnds = true;
		}
		protected override int GetCount(Agent pAgent)
		{
			return -1;
		}
	}

	class Condition_bt_Npc_RandomMove_Passive_node1 : behaviac.Condition
	{
		public Condition_bt_Npc_RandomMove_Passive_node1()
		{
		}
		protected override EBTStatus update_impl(behaviac.Agent pAgent, behaviac.EBTStatus childStatus)
		{
			bool opl = (bool)((Being)pAgent).IsSkilling();
			bool opr = false;
			bool op = opl == opr;
			return op ? EBTStatus.BT_SUCCESS : EBTStatus.BT_FAILURE;
		}
	}

	class Condition_bt_Npc_RandomMove_Passive_node2 : behaviac.Condition
	{
		public Condition_bt_Npc_RandomMove_Passive_node2()
		{
		}
		protected override EBTStatus update_impl(behaviac.Agent pAgent, behaviac.EBTStatus childStatus)
		{
			bool opl = (bool)((Being)pAgent).IsMoving();
			bool opr = true;
			bool op = opl == opr;
			return op ? EBTStatus.BT_SUCCESS : EBTStatus.BT_FAILURE;
		}
	}

	class Condition_bt_Npc_RandomMove_Passive_node4 : behaviac.Condition
	{
		public Condition_bt_Npc_RandomMove_Passive_node4()
		{
		}
		protected override EBTStatus update_impl(behaviac.Agent pAgent, behaviac.EBTStatus childStatus)
		{
			bool opl = (bool)((Being)pAgent).HasTarget();
			bool opr = true;
			bool op = opl == opr;
			return op ? EBTStatus.BT_SUCCESS : EBTStatus.BT_FAILURE;
		}
	}

	class Action_bt_Npc_RandomMove_Passive_node8 : behaviac.Action
	{
		public Action_bt_Npc_RandomMove_Passive_node8()
		{
		}
		protected override EBTStatus update_impl(behaviac.Agent pAgent, behaviac.EBTStatus childStatus)
		{
			behaviac.EBTStatus result = (behaviac.EBTStatus)((Npc)pAgent).AttackTarget();
			return result;
		}
	}

	class Action_bt_Npc_RandomMove_Passive_node5 : behaviac.Action
	{
		public Action_bt_Npc_RandomMove_Passive_node5()
		{
		}
		protected override EBTStatus update_impl(behaviac.Agent pAgent, behaviac.EBTStatus childStatus)
		{
			behaviac.EBTStatus result = (behaviac.EBTStatus)((Npc)pAgent).RandomMove();
			return result;
		}
	}

	public static class bt_Npc_RandomMove_Passive
	{
		public static bool build_behavior_tree(BehaviorTree bt)
		{
			bt.SetClassNameString("BehaviorTree");
			bt.SetId(-1);
			bt.SetName("Npc_RandomMove_Passive");
#if !BEHAVIAC_RELEASE
			bt.SetAgentType("Npc");
#endif
			// children
			{
				DecoratorLoop_bt_Npc_RandomMove_Passive_node6 node6 = new DecoratorLoop_bt_Npc_RandomMove_Passive_node6();
				node6.SetClassNameString("DecoratorLoop");
				node6.SetId(6);
#if !BEHAVIAC_RELEASE
				node6.SetAgentType("Npc");
#endif
				bt.AddChild(node6);
				{
					Sequence node7 = new Sequence();
					node7.SetClassNameString("Sequence");
					node7.SetId(7);
#if !BEHAVIAC_RELEASE
					node7.SetAgentType("Npc");
#endif
					node6.AddChild(node7);
					{
						Condition_bt_Npc_RandomMove_Passive_node1 node1 = new Condition_bt_Npc_RandomMove_Passive_node1();
						node1.SetClassNameString("Condition");
						node1.SetId(1);
#if !BEHAVIAC_RELEASE
						node1.SetAgentType("Npc");
#endif
						node7.AddChild(node1);
						node7.SetHasEvents(node7.HasEvents() | node1.HasEvents());
					}
					{
						IfElse node0 = new IfElse();
						node0.SetClassNameString("IfElse");
						node0.SetId(0);
#if !BEHAVIAC_RELEASE
						node0.SetAgentType("Npc");
#endif
						node7.AddChild(node0);
						{
							Condition_bt_Npc_RandomMove_Passive_node2 node2 = new Condition_bt_Npc_RandomMove_Passive_node2();
							node2.SetClassNameString("Condition");
							node2.SetId(2);
#if !BEHAVIAC_RELEASE
							node2.SetAgentType("Npc");
#endif
							node0.AddChild(node2);
							node0.SetHasEvents(node0.HasEvents() | node2.HasEvents());
						}
						{
							Sequence node3 = new Sequence();
							node3.SetClassNameString("Sequence");
							node3.SetId(3);
#if !BEHAVIAC_RELEASE
							node3.SetAgentType("Npc");
#endif
							node0.AddChild(node3);
							{
								Condition_bt_Npc_RandomMove_Passive_node4 node4 = new Condition_bt_Npc_RandomMove_Passive_node4();
								node4.SetClassNameString("Condition");
								node4.SetId(4);
#if !BEHAVIAC_RELEASE
								node4.SetAgentType("Npc");
#endif
								node3.AddChild(node4);
								node3.SetHasEvents(node3.HasEvents() | node4.HasEvents());
							}
							{
								Action_bt_Npc_RandomMove_Passive_node8 node8 = new Action_bt_Npc_RandomMove_Passive_node8();
								node8.SetClassNameString("Action");
								node8.SetId(8);
#if !BEHAVIAC_RELEASE
								node8.SetAgentType("Npc");
#endif
								node3.AddChild(node8);
								node3.SetHasEvents(node3.HasEvents() | node8.HasEvents());
							}
							node0.SetHasEvents(node0.HasEvents() | node3.HasEvents());
						}
						{
							Action_bt_Npc_RandomMove_Passive_node5 node5 = new Action_bt_Npc_RandomMove_Passive_node5();
							node5.SetClassNameString("Action");
							node5.SetId(5);
#if !BEHAVIAC_RELEASE
							node5.SetAgentType("Npc");
#endif
							node0.AddChild(node5);
							node0.SetHasEvents(node0.HasEvents() | node5.HasEvents());
						}
						node7.SetHasEvents(node7.HasEvents() | node0.HasEvents());
					}
					node6.SetHasEvents(node6.HasEvents() | node7.HasEvents());
				}
				bt.SetHasEvents(bt.HasEvents() | node6.HasEvents());
			}
			return true;
		}
	}

}
