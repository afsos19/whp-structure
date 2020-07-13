using System;
using System.Collections.Generic;
using System.Text;

namespace Whp.MaisTop.Business.Interfaces
{
    public interface IEmailService
    {
        bool SendSaleWhirpool(string network);
        bool SendSale(string month, int stepId, string email);
        bool SendHierarchyError(string email);
        bool SendHierarchySuccess(string monthYear, string email);
        bool SendSaleError(string email);
        bool SendSaleSuccess(string monthYear, string email);
        bool SendConfirmation(string login, string name, string email);
        bool SenderForgotPassword(string login, string name, string email, string password);
        bool SendAccessCodeExpiration(string code, string name, string email);
        bool SendAPasswordExpiration(string password, string name, string email);
        bool SendSKUEnabled(string networkName);
    }
}
