// 201902264:09 PM

using System;
using System.ComponentModel;
using System.Windows.Input;

namespace SimpleMVVM {
  public class CustomerViewModel  :ViewModelBase
  {
      private Customer obj = new Customer();
      private ButtonCommand objCommand; //  Point 1
      ButtonNewCommand newCommand;
      public CustomerViewModel() {
          objCommand = new ButtonCommand(this);
          newCommand = new ButtonNewCommand(Caculate, IsValid);
      }

      bool IsValid() {
         return obj.IsValid();
      }

      public bool IsMarried
      {
          get {
                if (obj.Married == "Married")
                {
                    return true;
                }
                else
                {
                    return false;
                }
          }
          set {
              if (value) {
                  obj.Married = "Married";
              }
              else {
                  obj.Married = "UnMarried";
              }
          }
      }

      public string TxtCustomerName
      {
          get { return obj.CustomerName; }
          set { obj.CustomerName = value; }
      }

      public string TxtAmount
      {
          get { return Convert.ToString(obj.Amount); }
          set { obj.Amount = Convert.ToDouble(value); }
      }
      public string LblAmountColor
      {
          get
          {
              if (obj.Amount > 2000)
              {
                  return "Blue";
              }
              else if (obj.Amount > 1500)
              {
                  return "Red";
              }
              return "Yellow";
          }
      }

      public string TxtTax {
          get {
              return Convert.ToString(obj.Tax);
          }
          set {
              var val = Convert.ToDouble(value);
              if (Math.Abs(obj.Tax - val) > double.Epsilon)
              {
                  obj.Tax = val;
                  OnPropertyChanged("TxtTax");
              }
              
              
          }
      }
      public void Caculate() {
          obj.CalculateTax();
          OnPropertyChanged("TxtTax");  
      }
      public ICommand BtnClick // Point 3
      {
          get
          {
              return newCommand;
          }
      }


  }
}
