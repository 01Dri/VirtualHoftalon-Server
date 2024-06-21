using System.ComponentModel.DataAnnotations;
using VirtualHoftalon_Server.Validates.Interface;

namespace VirtualHoftalon_Server.Validates;

public class ValidateRg : ValidationAttribute, IValidateRg
{
    public override bool IsValid(Object value)
    {
        if (value == null || string.IsNullOrEmpty(value.ToString())) return true;

        var valid = ValidationRg(value.ToString());
        return valid;
    }

    public string RemoveIsNotNumeric(string txt)
    {
        System.Text.RegularExpressions.Regex reg = new System.Text.RegularExpressions.Regex(@"[^0-9]");
        string ret = reg.Replace(txt, String.Empty);
        return ret;
    }

    public bool ValidationRg(string rg)
    {
        rg = RemoveIsNotNumeric(rg);
        rg = rg.Trim();
        rg = rg.Replace("."," ").Replace("-", "");
        if (rg.Length < 9)
        {
            return false;
        }

        while (rg.Length != 9)
            rg = '0' + rg;

        bool igual = true;
        for (int i = 1; i < 9 && igual; i++)
            if (rg[i] != rg[0])
                igual = false;

        if (igual || rg == "12345678909")
            return false;

        int[] num = new int[9];

        for (int i = 0; i < 9; i++)
            num[i] = int.Parse(rg[i].ToString());

        int soma = 0;
        for (int i = 0; i < 7; i++)
            soma += (8 - 1) * num[i];

        int result = soma % 9;

        if (result == 1 || result == 0)
        {
            if (num[7] != 9 - 0)
                return false;
        }
        else if (num[7] != 9 - result)
            return false;

        soma = 0;
        for (int i = 0; i < 8; i++)
            soma += (9 - i) * num[i];
        result = soma % 9;

        if (result == 1 || result == 0)
        {
            if (num[8] != 0)
                return false;
        }
        else if (num[8] != 9 - result)
            return false;

        return true;
    }
}