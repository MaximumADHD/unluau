// Copyright (c) Valence. All Rights Reserved.
// Licensed under the Apache License, Version 2.0

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Unluau
{
    public class Decleration
    {
        public static int LocalIdCounter = 0;
        public static int ClosureIdCounter = 0;
        public static int UpvalueIdCounter = 0;

        public enum DeclerationType
        {
            Local,
            Closure,
            Upvalue
        }

        private string _name;

        public string Name
        {
            get
            {
                if (_name is null)
                {
                    switch (Type)
                    {
                        case DeclerationType.Local:
                            return $"var{Id}";
                        case DeclerationType.Closure:
                            return $"fun{Id}";
                        case DeclerationType.Upvalue:
                            return $"upval{Id}";
                        default:
                            throw new Exception("Invalid DeclerationType");
                    }
                }
                return _name;
            }
        }
        public int Register { get; set; }
        public int Location { get; private set; }
        public int Referenced { get; set; } = 0;
        public DeclerationType Type { get; set; }
        public int Id { get; set; }

        public Decleration(LocalVariable localVariable) : this(localVariable.Slot, localVariable.Name, localVariable.StartPc)
        {
        }

        public Decleration(int register, int location, DeclerationType type)
        {
            Register = register;
            Location = location;
            Type = type;

            switch (type)
            {
                case DeclerationType.Local:
                {
                    Id = LocalIdCounter++;
                    break;
                }
                case DeclerationType.Closure:
                {
                    Id = ClosureIdCounter++;
                    break;
                }
                case DeclerationType.Upvalue:
                {
                    Id = UpvalueIdCounter++;
                    break;
                }
                default:
                {
                    throw new Exception("Invalid DeclerationType");
                }
            }
        }

        public Decleration(int register, string name, int location) : this(register, location, DeclerationType.Local)
        {
            _name = name;
        }

        public static void ResetCounters()
        {
            LocalIdCounter = 0;
            ClosureIdCounter = 0;
            UpvalueIdCounter = 0;
        }
    }
}
