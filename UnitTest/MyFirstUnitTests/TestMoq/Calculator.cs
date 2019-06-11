// 2019061114:07

namespace TestMoq {
    using System;

    public class Calculator:ICalculator {

        private IUSD_RMB_ExchangeRateFeed _feed;

        public Calculator(IUSD_RMB_ExchangeRateFeed feed)
        {
            this._feed = feed;
        }

        #region Implementation of ICalculator

        /// <inheritdoc />
        public int Add(int param1, int param2) {

            throw new NotImplementedException();
        }

        /// <inheritdoc />
        public int Subtract(int param1, int param2) {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        public int Multipy(int param1, int param2) {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        public int Divide(int param1, int param2) {
            return param1 / param2;
        }

        /// <inheritdoc />
        public int ConvertUSDtoRMB(int unit) {
            return unit * this._feed.GetActualUSDValue();
        }

        #endregion
    }
}
