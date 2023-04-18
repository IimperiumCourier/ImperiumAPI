using ImperiumLogistics.Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImperiumLogistics.Infrastructure.Abstract
{
    public interface IEmailService
    {
        Task SendMail(EmailRequestDto requestDto);
    }
}
