// 2019061114:10

namespace TestMoq {
    using System;

    using Moq;

    using Xunit;

    public class CalculatorTester {
        /// <summary>
        /// 定义mock的逻辑
        /// </summary>
        /// <returns>
        /// The <see cref="IUSD_RMB_ExchangeRateFeed"/>.
        /// </returns>
        private IUSD_RMB_ExchangeRateFeed PrvGetExchangeRateFeed() {
            Mock<IUSD_RMB_ExchangeRateFeed> mockObject = new Mock<IUSD_RMB_ExchangeRateFeed>();
            mockObject.Setup(m => m.GetActualUSDValue()).Returns(500);
            return mockObject.Object;
        }

        [Fact]
        public void TC1_Divide9By3() {
            IUSD_RMB_ExchangeRateFeed feed = PrvGetExchangeRateFeed();
            ICalculator calculator = new Calculator(feed);
            int actualResult = calculator.Divide(9, 3);
            int expectedResult = 3;
            Assert.Equal(expectedResult, actualResult);
        }

        [Fact]
        public void TC2_DivideByZero()
        {
            IUSD_RMB_ExchangeRateFeed feed = this.PrvGetExchangeRateFeed();
            ICalculator calculator = new Calculator(feed);
            int actualResult = calculator.Divide(9, 0);
            Assert.Throws<InvalidOperationException>(() => operation()); 
        }

        void operation() 
        { 
            throw new InvalidOperationException(); 
        } 

        [Fact]
        public void TC3_ConvertUSDtoRMBTest()
        {
            IUSD_RMB_ExchangeRateFeed feed = this.PrvGetExchangeRateFeed();
            ICalculator calculator = new Calculator(feed);
            int actualResult = calculator.ConvertUSDtoRMB(1);
            int expectedResult = 500;
            Assert.Equal(expectedResult, actualResult);
        }
    }
}
