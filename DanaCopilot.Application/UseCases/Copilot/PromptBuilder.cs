using System;
using System.Collections.Generic;
using System.Text;

namespace DanaCopilot.Application.UseCases.Copilot
{
    public class PromptBuilder
    {
        public string Build(PromptContext context)
        {
            return $"""
شما یک کارشناس خدمات پس از فروش لوازم خانگی هستید.

فقط بر اساس اطلاعات زیر پاسخ بده.

اگر پاسخ در اطلاعات وجود نداشت
صادقانه اعلام کن که اطلاعات کافی وجود ندارد.

-----------------------

اطلاعات مرجع:

{context.ContextText}

-----------------------

سوال:

{context.Question}

-----------------------

پاسخ:
""";
        }
    }
}
