using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


    interface IEventExecute
    {

    void Update(float f_Delta,bool a_bIsEnd);
    void Render();
    void Init();
}
