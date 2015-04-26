using System;
using System.Collections.ObjectModel;
using System.Linq.Expressions;
using System.Reflection;
using NHibernate.Hql.Ast;
using NHibernate.Linq;
using NHibernate.Linq.Functions;
using NHibernate.Linq.Visitors;

namespace LiteFx.Context.NHibernate
{
	public class ExtendedEqualsGenerator : BaseHqlGeneratorForMethod
	{
		public ExtendedEqualsGenerator()
		{
			// the methods call are used only to get info about the signature, the actual parameter is just ignored
			SupportedMethods = new[] { 
			ReflectionHelper.GetMethodDefinition<IEquatable<Byte>>(x => x.Equals((Byte)0)),
			ReflectionHelper.GetMethodDefinition<IEquatable<SByte>>(x => x.Equals((SByte)0)),
			ReflectionHelper.GetMethodDefinition<IEquatable<Int16>>(x => x.Equals((Int16)0)),
			ReflectionHelper.GetMethodDefinition<IEquatable<Int32>>(x => x.Equals((Int32)0)),
			ReflectionHelper.GetMethodDefinition<IEquatable<Int64>>(x => x.Equals((Int64)0)),
			ReflectionHelper.GetMethodDefinition<IEquatable<UInt16>>(x => x.Equals((UInt16)0)),
			ReflectionHelper.GetMethodDefinition<IEquatable<UInt32>>(x => x.Equals((UInt32)0)),
			ReflectionHelper.GetMethodDefinition<IEquatable<UInt64>>(x => x.Equals((UInt64)0)),
			ReflectionHelper.GetMethodDefinition<IEquatable<Single>>(x => x.Equals((Single)0)),
			ReflectionHelper.GetMethodDefinition<IEquatable<Double>>(x => x.Equals((Double)0)),
			ReflectionHelper.GetMethodDefinition<IEquatable<Boolean>>(x => x.Equals(true)),
			ReflectionHelper.GetMethodDefinition<IEquatable<Char>>(x => x.Equals((Char)0)),
			ReflectionHelper.GetMethodDefinition<IEquatable<Decimal>>(x => x.Equals((Decimal)0)),
			ReflectionHelper.GetMethodDefinition<IEquatable<Guid>>(x => x.Equals(Guid.Empty)),
			ReflectionHelper.GetMethodDefinition<IEquatable<string>>(x => x.Equals(string.Empty))
			};
		}
		
		public override HqlTreeNode BuildHql(MethodInfo method, Expression targetObject, ReadOnlyCollection<Expression> arguments, HqlTreeBuilder treeBuilder, IHqlExpressionVisitor visitor)
		{
			return treeBuilder.Equality(
					visitor.Visit(targetObject).AsExpression(),
					visitor.Visit(arguments[0]).AsExpression());
		}
	}

	public class ExtendedLinqtoHqlGeneratorsRegistry : DefaultLinqToHqlGeneratorsRegistry
	{
		public ExtendedLinqtoHqlGeneratorsRegistry() : base()
		{
			this.Merge(new ExtendedEqualsGenerator());
		}
	}
}
