using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace CorporateWebProject.Application.Utilities.ExpressionNormalizer
{
    public static class ExpressionNormalizer
    {
        public static string NormalizeExpression<T>(Expression<Func<T, bool>> expression)
        {
            // ExpressionVisitor kullanarak sabit ve dinamik değerleri topluyoruz
            var visitor = new ParameterExpressionVisitor();
            visitor.Visit(expression);

            // Normalleştirilmiş expression'ı ve dinamik parametreleri birleştiriyoruz
            string expressionString = expression.ToString();
            string paramValues = string.Join(",", visitor.ParameterValues.Select(x => $"{x.Key}:{x.Value}"));

            return expressionString + paramValues;
        }

        private static string ExpressionToString(Expression expression)
        {
            // Expression'ı basit bir string'e çeviriyoruz (daha gelişmiş bir çözüm gerekirse özelleştirilebilir)
            return expression.ToString();
        }

        private class ParameterExpressionVisitor : ExpressionVisitor
        {
            public Dictionary<string, object> ParameterValues { get; } = new Dictionary<string, object>();

            protected override Expression VisitMember(MemberExpression node)
            {
                // Dinamik değişkenlerin değerlerini topluyoruz
                if (node.Expression is ConstantExpression constantExpression)
                {
                    var value = GetValue(constantExpression);
                    ParameterValues[node.Member.Name] = value;
                }
                return base.VisitMember(node);
            }

            private object GetValue(ConstantExpression constant)
            {
                var fieldInfo = constant.Type.GetFields().FirstOrDefault();
                return fieldInfo?.GetValue(constant.Value) ?? constant.Value;
            }
        }
    }
}
