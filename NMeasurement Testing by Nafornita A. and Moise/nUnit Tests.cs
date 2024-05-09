using NMeasure;
using Testare_Moise_Nafornita;

namespace NMeasurement_Testing_by_Nafornita_A._and_Moise
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void Test1()
        {
            Assert.Pass();
        }

        // Equivalence Partitioning test
        // input: x ∈ N,Q,Z; mu is a measurement unit
        [Test]
        public void AdditionIntegerNegativeDecimal_ReturnsValidResult()
        {
            StandardUnitConfiguration.Use();
            var dist1 = new Measure(500, U.Meter);
            var dist2 = new Measure(-5.0m, U.Kilometer);
            var dist3 = dist1 + dist2;
            Assert.AreEqual(new Measure(-4500, U.Meter), dist3);
        }

        [Test]
        public void AdditionIntegerInteger_ReturnsValidResult()
        {
            StandardUnitConfiguration.Use();
            var dist1 = new Measure(500, U.Meter);
            var dist2 = new Measure(5, U.Kilometer);
            var dist3 = dist1 + dist2;
            Assert.AreEqual(new Measure(5500, U.Meter), dist3);
        }

        [Test]
        public void AdditionDecimalDecimal_ReturnsValidResult()
        {
            StandardUnitConfiguration.Use();
            var dist1 = new Measure(100.0m, U.Meter);
            var dist2 = new Measure(1.0m, U.Kilometer);
            var dist3 = dist1 + dist2;
            Assert.AreEqual(new Measure(1100.0m, U.Meter), dist3);
        }
        // Equivalence Partitioning test
        // input: x ∈ N,Q,Z; mu is a measurement unit


        // Boundary Value Analysis
        // Converting from Kilometer to Meter
        [Test]
        public void ConvertKilometerToMeter_UpperValidBoundary_ReturnsValidResult()
        {
            StandardUnitConfiguration.Use();
            var dist1 = new Measure(decimal.MaxValue / 1000, U.Kilometer);
            try
            {
                var dist2 = dist1.ConvertTo(U.Meter);
                Assert.Pass();
            }
            catch (OverflowException)
            {
                Assert.Fail();
            }
        }

        [Test]
        public void ConvertKilometerToMeter_UpperInvalidBoundary_ThrowsOverflowException()
        {
            StandardUnitConfiguration.Use();
            var dist1 = new Measure(decimal.MaxValue / 1000 + 1, U.Kilometer);
            try
            {
                var dist2 = dist1.ConvertTo(U.Meter);
                Assert.Fail();
            }
            catch (OverflowException)
            {
                Assert.Pass();
            }
        }

        public void ConvertKilometerToMeter_LowerValidBoundary_ReturnsValidResult()
        {
            StandardUnitConfiguration.Use();
            var dist1 = new Measure(decimal.MinValue / 1000, U.Kilometer);
            try
            {
                var dist2 = dist1.ConvertTo(U.Meter);
                Assert.Pass();
            }
            catch (OverflowException)
            {
                Assert.Fail();
            }
        }

        [Test]
        public void ConvertKilometerToMeter_LowerInvalidBoundary_ThrowsOverflowException()
        {
            StandardUnitConfiguration.Use();
            var dist1 = new Measure(decimal.MinValue / 1000 - 1, U.Kilometer);
            try
            {
                var dist2 = dist1.ConvertTo(U.Meter);
                Assert.Fail();
            }
            catch (OverflowException)
            {
                Assert.Pass();
            }
        }
        // Boundary Value Analysis
        // Converting from Kilometer to Meter

        // Statement Coverage
        [Test]
        public void WhereAreYouGoingToday_ShortDistanceSlowSpeedHatchback_ReturnsWalk()
        {
            DistanceToCoverService service = new DistanceToCoverService();
            var result = service.WhereAreYouGoingToday(Distance.Short, Speed.Slow, Car.Hatchback);
            Assert.AreEqual("You should walk", result);
        }
        [Test]
        public void WhereAreYouGoingToday_ShortDistanceSlowSpeedSUV_ReturnsBus()
        {
            DistanceToCoverService service = new DistanceToCoverService();
            var result = service.WhereAreYouGoingToday(Distance.Short, Speed.Slow, Car.SUV);
            Assert.AreEqual("You should take the bus", result);
        }
        [Test]
        public void WhereAreYouGoingToday_ShortDistanceFastSpeedHatchback_ReturnsDrive()
        {
            DistanceToCoverService service = new DistanceToCoverService();
            var result = service.WhereAreYouGoingToday(Distance.Short, Speed.Fast, Car.Hatchback);
            Assert.AreEqual("You should drive", result);
        }
        [Test]
        public void WhereAreYouGoingToday_ShortDistanceFastSpeedSUV_ReturnsTaxi()
        {
            DistanceToCoverService service = new DistanceToCoverService();
            var result = service.WhereAreYouGoingToday(Distance.Short, Speed.Fast, Car.SUV);
            Assert.AreEqual("You should take a taxi", result);
        }
        
        [Test]
        public void WhereAreYouGoingToday_LongDistance_ReturnsEmpty()
        {
            DistanceToCoverService service = new DistanceToCoverService();
            var result = service.WhereAreYouGoingToday(Distance.Long, Speed.Slow, Car.Hatchback);
            Assert.AreEqual(string.Empty, result);
        }
        // Statement Coverage


        // COVERAGE TESTS
        [Test]
        public void PumpGas_InvalidOption_SaysIOE()
        {
            var pumpGasService = new PumpGasService(100m, 7.5m);
            pumpGasService.PumpGas("3");
        }

        [Test]
        public void PupGas_FillTankWithAllMoney_ReturnsFilledWithWhatWasLeft()
        {
            var pumpGasService = new PumpGasService(100m, 7.5m);
            var result = pumpGasService.PumpGas("1");
            Assert.AreEqual("Filled with what was left in your bank account.", result);
        }

        [Test]
        public void PumpGas_FillTankWithSpecificAmount_ReturnsFilledUpWithSpecifiedAmount()
        {
            var pumpGasService = new PumpGasService(100m, 7.5m);
            var result = pumpGasService.PumpGas("2", 10);
            Assert.AreEqual("Filled up with specified amount of gas!", result);
        }

        [Test]
        public void PumpGas_NotEnoughMoney_ReturnsNotEnoughMoney()
        {
            var pumpGasService = new PumpGasService(10m, 7.5m);
            var result = pumpGasService.PumpGas("2", 10);
            Assert.AreEqual("You don't have enough money to buy this amount of gas.", result);
        }

        [Test]
        public void PumpGas_FillTankWithAllMoney_ReturnsTankFilledUp()
        {
            var pumpGasService = new PumpGasService(1000m, 7.5m);
            var result = pumpGasService.PumpGas("1");
            Assert.AreEqual("Tank filled up!", result);
        }
        // COVERAGE TESTS -- may view in ./reports/index.html Genereaza date de test care testează cazurile când fiecare decizie este adevărată sau falsă.

        // decision coverage
        [Test]
        public void PumpGasDiesel_FillTankWithAllMoney_ReturnsTankFilledUp()
        {
            var pumpGasService = new PumpGasService(1000m, 7.5m);
            var result = pumpGasService.PumpGas("1", 1, "diesel");
            Assert.AreEqual("Tank filled up!", result);
        }

        [Test]
        public void PumpGasKersoene_FillTankWithAllMoney_ReturnsWrongFuelType()
        {
            var pumpGasService = new PumpGasService(1000m, 7.5m);
            var result = pumpGasService.PumpGas("1", 1, "kerosene");
            Assert.AreEqual("Wrong fuel type!", result);
        }
        // decision coverage - condițiile individuale c1 și c2 sa ia atat valoarea adevărat
        // cat și valoarea fals

        //Kill mutant -> if (fuelType=="diesel" || fuelType == "petrol")
        // here "petrol" is changed to ""
        [Test]
        public void PumpPetrol_FillTankWithAllMoney_ReturnsTankFilledUp()
        {
            var pumpGasService = new PumpGasService(1000m, 7.5m);
            var result = pumpGasService.PumpGas("1", 1, "petrol");
            Assert.AreEqual("Tank filled up!", result);
        }

        [Test]
        public void PumpPetrol_InvalidOperation_ReturnsIOE()
        {
            var pumpGasService = new PumpGasService(1000m, 7.5m);
            var result = pumpGasService.PumpGas(int.MaxValue.ToString(), 1, "petrol");
            Assert.AreEqual("IOE", result);
        }
    }
}