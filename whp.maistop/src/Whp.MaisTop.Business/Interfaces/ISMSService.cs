using System;
using System.Collections.Generic;
using System.Text;

namespace Whp.MaisTop.Business.Interfaces
{
    public interface ISMSService
    {
        (string Id, string Code, string Text) Send(string cellphone, string link);
        (string Id, string Code, string Text) SendConfirmation(string cellphone, bool indicator);
        (string Phone, string Status) VerifySend(string sendCode);
        (string Id, string Code, string Text) SendForgotPassword(string cellphone, string password);
        (string Id, string Code, string Text) SendAccessConfirmation(string cellphone, string code);
        (string Id, string Code, string Text) SendAccessInvite(string celular, string codigo);
    }
}
