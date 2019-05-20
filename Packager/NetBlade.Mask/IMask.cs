using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetBlade.Mask
{
    public interface IMask
    {
        string CleanValue(string value);

        string Format(string value);
    }
}
