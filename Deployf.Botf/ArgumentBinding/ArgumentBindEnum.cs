﻿using System.Reflection;
using Telegram.Bot.Framework.Abstractions;
#if NET5_0
    using ValueTask = System.Threading.Tasks.ValueTask;
    using ValueTaskGeneric = System.Threading.Tasks.ValueTask<object>;
#else
using ValueTask = System.Threading.Tasks.Task;
using ValueTaskGeneric = System.Threading.Tasks.Task<object>;
#endif

namespace Deployf.Botf;

public class ArgumentBindEnum : IArgumentBind
{
    public bool CanDecode(ParameterInfo parameter, object argument)
    {
        return parameter.ParameterType.IsEnum;
    }

    public bool CanEncode(ParameterInfo parameter, object argument)
    {
        return parameter.ParameterType.IsEnum;
    }

    public ValueTaskGeneric Decode(ParameterInfo parameter, object argument, IUpdateContext _)
    {
        var str = argument.ToString();
        if (Enum.TryParse(parameter.ParameterType, str, out var result))
        {
#if NET5_0
            return new(result!);
#else
            return ValueTask.FromResult<object>(result!);
#endif
            
        }
        return ValueTask.FromException<object>(new NotImplementedException("enum conversion for current data is not implemented"));
    }

    public string Encode(ParameterInfo parameter, object argument, IUpdateContext _)
    {
        return Enum.Format(parameter.ParameterType, argument, "D");
    }
}
