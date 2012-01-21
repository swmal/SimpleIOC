using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Linq.Expressions;
using System.Reflection;

namespace SimpleIOC.Core.Activation
{
    public class ExpressionActivator : ITypeActivator
    {
        private readonly IConstructorSelector _constructorSelector;
        private readonly Container _container;
        private Dictionary<Type, Delegate> _lambdas = new Dictionary<Type, Delegate>();

        public ExpressionActivator(Container container, IConstructorSelector constructorSelector)
        {
            _constructorSelector = constructorSelector;
            _container = container;
        }

        public object CreateInstance(Type type)
        {
            if (!_lambdas.ContainsKey(type))
            {
                var constructorInfo = _constructorSelector.Select(type, _container);
                var arguments = GetArguments(constructorInfo).ToArray();
                var ctorCall = Expression.New(constructorInfo, arguments);
                var lambda = Expression.Lambda(ctorCall, Enumerable.Empty<ParameterExpression>());
                _lambdas.Add(type, lambda.Compile());
            }
            return _lambdas[type].DynamicInvoke();
        }

        private IEnumerable<Expression> GetArguments(ConstructorInfo constructorInfo)
        {
            foreach (var info in constructorInfo.GetParameters())
            {
                var type = info.ParameterType;
                var item = _container.Resolve(type);
                yield return Expression.Constant(item);
            }
        }
    }
}
