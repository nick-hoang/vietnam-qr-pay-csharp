using vietnam_qr_pay_csharp;

namespace vietnam_qr_pay_csharp.Test
{
    [TestClass]
    public class VietQRUnitTest
    {
        [TestMethod]
        public void TestVietQR()
        {
            const string qrContent = "00020101021238530010A0000007270123000697041601092576788590208QRIBFTTA5303704540410005802VN62150811Chuyen tien6304BBB8";
            var qrPay = new QRPay(qrContent);
            Assert.IsTrue(qrPay.isValid);
            Assert.IsTrue(qrPay.version == "01");
            Assert.IsTrue(qrPay.provider.name == QRProvider.VIETQR);
            Assert.IsTrue(qrPay.provider.guid == QRProviderGUID.VIETQR);
            Assert.IsTrue(qrPay.consumer.bankBin == "970416");
            Assert.IsTrue(qrPay.consumer.bankNumber == "257678859");
            Assert.IsTrue(qrPay.amount == "1000");
            Assert.IsTrue(qrPay.Build() == qrContent);
        }

        [TestMethod]
        public void TestCRC_With_Three_Byte()
        {
            const string qrContent = "00020101021138580010A000000727012800069704070114190304136010180208QRIBFTTA53037045802VN63040283";
            var qrPay = new QRPay(qrContent);
            Assert.IsTrue(qrPay.isValid == true);
            Assert.IsTrue(qrPay.version == "01");
            Assert.IsTrue(qrPay.provider.name == QRProvider.VIETQR);
            Assert.IsTrue(qrPay.provider.guid == QRProviderGUID.VIETQR);
            Assert.IsTrue(qrPay.consumer.bankBin == "970407");
            Assert.IsTrue(qrPay.consumer.bankNumber == "19030413601018");
            Assert.IsTrue(qrPay.Build() == qrContent);
        }

        [TestMethod]
        public void TestInvalid_CRC_VietQR()
        {
            var qrPay = new QRPay("00020101021238530010A0000007270123000697041601092576788590208QRIBFTTA5303704540410005802VN62150811Chuyen tien6304BBB5");
            Assert.IsFalse(qrPay.isValid);
        }

        [TestMethod]
        public void TestMBBank_QR_with_LowercaseCRC()
        {
            const string qrContent = "00020101021138540010A00000072701240006970422011003523509170208QRIBFTTA53037045802VN630479db";
            var qrPay = new QRPay(qrContent);
            Assert.IsTrue(qrPay.isValid == true);
            Assert.IsTrue(qrPay.version == "01");
            Assert.IsTrue(qrPay.provider.name == QRProvider.VIETQR);
            Assert.IsTrue(qrPay.provider.guid == QRProviderGUID.VIETQR);
            Assert.IsTrue(qrPay.consumer.bankBin == "970422");
            Assert.IsTrue(qrPay.consumer.bankNumber == "0352350917");

            //Assert.IsTrue(qrPay.build()?.slice(-4) == qrContent?.slice(-4).toUpperCase());
            Assert.IsTrue(string.Equals(qrPay.Build()?.Substring(Math.Max(0, qrPay.Build().Length - 4)),
              qrContent?.Substring(Math.Max(0, qrContent.Length - 4))?.ToUpper(),
              StringComparison.Ordinal));
        }
    }
}