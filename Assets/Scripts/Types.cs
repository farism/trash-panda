public interface IScreen
{
  void Show() { }
  void Hide() { }
}

public enum View
{
  Home,
  Shop,
  Jobs,
  Job,
}

public enum JobType
{
  City,
  Desert,
  Meadow,
}

public enum JobDifficulty
{
  Easy,
  Medium,
  Hard,
}

public enum JobTimezone
{
  UTCMinus12,
  UTCMinus11,
  UTCMinus10,
  UTCMinus9,
  UTCMinus8,
  UTCMinus7,
  UTCMinus6,
  UTCMinus5,
  UTCMinus4,
  UTCMinus3,
  UTCMinus2,
  UTCMinus1,
  UTCZero,
  UTCPlus1,
  UTCPlus2,
  UTCPlus3,
  UTCPlus4,
  UTCPlus5,
  UTCPlus6,
  UTCPlus7,
  UTCPlus8,
  UTCPlus9,
  UTCPlus10,
  UTCPlus11,
  UTCPlus12,
}

public enum TrashType
{
  Apple,
  CardboardBox,
  Carton,
  Cone,
  Cone2,
  Couch,
  FoodCan,
  Fridge,
  Gear,
  Microwave,
  Muffler,
  PaperBag,
  PizzaBox,
  Plate,
  RedCup,
  Sign,
  SodaBottle,
  SodaCan,
  SodaCup,
  Strut,
  Table,
  Tire,
  WaterBottle,
}