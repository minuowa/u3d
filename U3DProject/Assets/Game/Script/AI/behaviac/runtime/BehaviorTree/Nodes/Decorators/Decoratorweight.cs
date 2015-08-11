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
using System.Collections;
using System.Collections.Generic;

namespace behaviac
{
    public class DecoratorWeight : DecoratorNode
    {
        public DecoratorWeight()
        {
		}

        ~DecoratorWeight()
        {
            m_weight_var = null;
        }

        protected override void load(int version, string agentType, List<property_t> properties)
        {
            base.load(version, agentType, properties);

            foreach (property_t p in properties)
            {
                if (p.name == "Weight")
                {
                    string typeName = null;
                    string propertyName = null;
                    this.m_weight_var = Condition.LoadRight(p.value, propertyName, ref typeName);
                }
            }
        }

        protected virtual int GetWeight(behaviac.Agent pAgent)
        {
            if (this.m_weight_var != null)
            {
                Debug.Check(this.m_weight_var != null);
                int count = (int)this.m_weight_var.GetValue(pAgent);

                return count;
            }

            return 0;
        }

        protected override BehaviorTask createTask()
        {
            DecoratorWeightTask pTask = new DecoratorWeightTask();

            return pTask;
        }

        Property m_weight_var;

        public class DecoratorWeightTask : DecoratorTask
        {
            public DecoratorWeightTask() : base()
            {
            }

            public int GetWeight(Agent pAgent)
            {
                Debug.Check(this.GetNode() is DecoratorWeight);
                DecoratorWeight pNode = (DecoratorWeight)(this.GetNode());

                return pNode != null ? pNode.GetWeight(pAgent) : 0;
            }

            public override void copyto(BehaviorTask target)
            {
                base.copyto(target);
            }

            public override void save(ISerializableNode node)
            {
                base.save(node);
            }

            public override void load(ISerializableNode node)
            {
                base.load(node);
            }

            protected override EBTStatus decorate(EBTStatus status)
            {
                return status;
            }
        }
    }
}