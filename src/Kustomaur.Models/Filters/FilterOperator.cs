using System;
using System.Runtime.Serialization;

namespace Kustomaur.Models.Filters;

public enum FilterOperator
{
    Equals = 0,
    NotEquals = 1,
    StartsWith = 3
}