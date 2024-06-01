using WebApplication1.Models;
using WebApplication1.Models.ChoiceHelpers;
using WebApplication1.Models.ChoiceHelpers.Conditions;
using WebApplication1.Models.MotorOilStats;

namespace WebApplication1.TestUtils
{
    public class TestDBMaker
    {
        public bool testModeOn = true;
        public ApplicationContext db;
        public void initTestDb()
        {

            List<APIQualityClass> APIclasses = new List<APIQualityClass>();
            APIclasses.Add(MakeQC("SA", 1));
            APIclasses.Add(MakeQC("SB", 2));
            APIclasses.Add(MakeQC("SC", 3));
            APIclasses.Add(MakeQC("SD", 4));
            APIclasses.Add(MakeQC("SE", 5));
            APIclasses.Add(MakeQC("SF", 6));
            APIclasses.Add(MakeQC("SG", 7));
            APIclasses.Add(MakeQC("SH", 8));
            APIclasses.Add(MakeQC("SJ", 9));
            APIclasses.Add(MakeQC("SL", 10));
            APIclasses.Add(MakeQC("SM", 11));
            APIclasses.Add(MakeQC("SN", 12));
            APIclasses.Add(MakeQC("SP", 13));
            List<MotorOil> motorOils = initMotorOils(APIclasses);
            //Добавить: Stores, Companies, Merches
            List<Company> companies = makeCompanies();
            List<Store> stores = new List<Store>();
            stores.Add(makeStore(companies[1], "г. Челябинск, ул. Ворошилова, д. 22 к. 2"));
            stores.Add(makeStore(companies[1], "г. Челябинск, ул. Цвиллинга, д. 108"));
            stores.Add(makeStore(companies[1], "г. Челябинск, ул. Братьев Кашириных, д. 68"));
            stores.Add(makeStore(companies[2], "г. Челябинск, ул. Воровского, д. 81"));
            stores.Add(makeStore(companies[0], "г. Челябинск, ул. Каслинская, д. 12"));

            List<MotorOilMerch> merches = new List<MotorOilMerch>();
            merches.Add(makeMerch(motorOils[0], stores[0], 2272, 31));
            merches.Add(makeMerch(motorOils[1], stores[3], 2283, 263));
            merches.Add(makeMerch(motorOils[2], stores[2], 1578, 113));
            merches.Add(makeMerch(motorOils[3], stores[4], 1383, 53));
            merches.Add(makeMerch(motorOils[4], stores[1], 1555, 87));

            merches.Add(makeMerch(motorOils[5], stores[1], 2219, 23));
            merches.Add(makeMerch(motorOils[6], stores[2], 1885, 63));
            merches.Add(makeMerch(motorOils[7], stores[2], 709, 723));
            merches.Add(makeMerch(motorOils[8], stores[1], 1703, 43));
            merches.Add(makeMerch(motorOils[9], stores[3], 1874, 73));

            merches.Add(makeMerch(motorOils[10], stores[4], 1776, 67));
            merches.Add(makeMerch(motorOils[11], stores[0], 1359, 48));
            merches.Add(makeMerch(motorOils[12], stores[0], 2100, 69));
            merches.Add(makeMerch(motorOils[13], stores[3], 1393, 36));
            merches.Add(makeMerch(motorOils[14], stores[2], 1569, 86));
            merches.Add(makeMerch(motorOils[15], stores[3], 875, 47));

            merches.Add(makeMerch(motorOils[16], stores[1], 2943, 596));
            merches.Add(makeMerch(motorOils[17], stores[1], 2418, 5));
            merches.Add(makeMerch(motorOils[18], stores[4], 2618, 34));
            merches.Add(makeMerch(motorOils[19], stores[3], 2152, 56));
            merches.Add(makeMerch(motorOils[20], stores[0], 849, 97));


            // Conds, ConditionSets, CarTypes, MotorTypes
            List<SAEViscosityCondition> SAEConds = new List<SAEViscosityCondition>();
            List<APIQualityCondition> APIConds = new List<APIQualityCondition>();
            //условно для отечественных sg+, для иномарок sl+ (класс качества)
            List<ConditionsSet> condSets = new List<ConditionsSet>();
            for(int i=0; i<17; i++)
            {
                condSets.Add(new ConditionsSet());
            }

            List<CarType> cars = new List<CarType>();
            cars.Add(makeCar("ВАЗ Granta 2014", condSets[0]));
            cars.Add(makeCar("Hyundai Solaris 2015", condSets[1]));
            cars.Add(makeCar("Hyundai Solaris 2016", condSets[2]));
            cars.Add(makeCar("Kia Rio 3 2014", condSets[3]));
            cars.Add(makeCar("Kia Rio 3 2015", condSets[4]));
            cars.Add(makeCar("Kia Rio 3 2016", condSets[5]));
            cars.Add(makeCar("ВАЗ Kalina 2 2014", condSets[6]));
            cars.Add(makeCar("Volkswagen Polo 2014", condSets[7]));
            cars.Add(makeCar("Volkswagen Polo 2015", condSets[8]));
            cars.Add(makeCar("Volkswagen Polo 2016", condSets[9]));
            cars.Add(makeCar("Renault Logan 2 2014", condSets[10]));
            cars.Add(makeCar("Renault Logan 2 2015", condSets[11]));
            cars.Add(makeCar("Renault Logan 2 2016", condSets[12]));
            cars.Add(makeCar("Renault Duster 2014", condSets[13]));
            cars.Add(makeCar("Renault Duster 2015", condSets[14]));
            cars.Add(makeCar("Renault Duster 2016", condSets[15]));
            cars.Add(makeCar("ВАЗ Largus 2014", condSets[16]));

            //ВАЗ гранта 2014
            SAEConds.Add(makeSAECond(5, 10, 40, 40, 1, true));
            SAEConds.Add(makeSAECond(0, 10, 30, 30, 2, true));
            APIConds.Add(makeAPICond(APIclasses[6], 3, true));

            SAEConds[0].ConditionsSet = condSets[0];
            SAEConds[1].ConditionsSet = condSets[0];
            APIConds[0].ConditionsSet = condSets[0];

            //хёндай солярис 2015
            SAEConds.Add(makeSAECond(0, 5, 40, 40, 1, true));
            APIConds.Add(makeAPICond(APIclasses[9], 2, true));

            SAEConds[2].ConditionsSet = condSets[1];
            APIConds[1].ConditionsSet = condSets[1];

            //хёндай солярис 2016
            SAEConds.Add(makeSAECond(0, 0, 40, 40, 1, true));
            SAEConds.Add(makeSAECond(5, 5, 50, 50, 2, true));
            APIConds.Add(makeAPICond(APIclasses[9], 3, true));

            SAEConds[3].ConditionsSet = condSets[2];
            SAEConds[4].ConditionsSet = condSets[2];
            APIConds[2].ConditionsSet = condSets[2];

            //киа рио 3 2014
            SAEConds.Add(makeSAECond(5, 10, 40, 40, 1, true));
            SAEConds.Add(makeSAECond(10, 10, 30, 30, 2, true));
            APIConds.Add(makeAPICond(APIclasses[9], 3, true));

            SAEConds[5].ConditionsSet = condSets[3];
            SAEConds[6].ConditionsSet = condSets[3];
            APIConds[3].ConditionsSet = condSets[3];

            //киа рио 3 2015
            SAEConds.Add(makeSAECond(0, 5, 40, 40, 1, true));
            APIConds.Add(makeAPICond(APIclasses[9], 2, true));

            SAEConds[7].ConditionsSet = condSets[4];
            APIConds[4].ConditionsSet = condSets[4];

            //киа рио 3 2016
            SAEConds.Add(makeSAECond(0, 0, 40, 40, 1, true));
            SAEConds.Add(makeSAECond(5, 5, 50, 50, 2, true));
            APIConds.Add(makeAPICond(APIclasses[9], 3, true));

            SAEConds[8].ConditionsSet = condSets[5];
            SAEConds[9].ConditionsSet = condSets[5];
            APIConds[5].ConditionsSet = condSets[5];

            //ваз калина 2 2014
            SAEConds.Add(makeSAECond(5, 10, 40, 40, 1, true));
            SAEConds.Add(makeSAECond(10, 10, 30, 30, 2, true));
            APIConds.Add(makeAPICond(APIclasses[6], 3, true));

            SAEConds[10].ConditionsSet = condSets[6];
            SAEConds[11].ConditionsSet = condSets[6];
            APIConds[6].ConditionsSet = condSets[6];

            //фольксваген поло 2 2014
            SAEConds.Add(makeSAECond(5, 5, 40, 40, 1, true));
            SAEConds.Add(makeSAECond(10, 10, 30, 40, 2, true));
            APIConds.Add(makeAPICond(APIclasses[9], 3, true));

            SAEConds[12].ConditionsSet = condSets[7];
            SAEConds[13].ConditionsSet = condSets[7];
            APIConds[7].ConditionsSet = condSets[7];

            //фольксваген поло 2 2015
            SAEConds.Add(makeSAECond(0, 5, 40, 40, 1, true));
            APIConds.Add(makeAPICond(APIclasses[9], 2, true));

            SAEConds[14].ConditionsSet = condSets[8];
            APIConds[8].ConditionsSet = condSets[8];

            //фольксваген поло 2 2016
            SAEConds.Add(makeSAECond(0, 0, 40, 40, 1, true));
            SAEConds.Add(makeSAECond(5, 5, 50, 50, 2, true));
            APIConds.Add(makeAPICond(APIclasses[9], 3, true));

            SAEConds[15].ConditionsSet = condSets[9];
            SAEConds[16].ConditionsSet = condSets[9];
            APIConds[9].ConditionsSet = condSets[9];

            //рено логан 2 2014
            SAEConds.Add(makeSAECond(5, 10, 40, 40, 1, true));
            APIConds.Add(makeAPICond(APIclasses[9], 2, true));

            SAEConds[17].ConditionsSet = condSets[10];
            APIConds[10].ConditionsSet = condSets[10];

            //рено логан 2 2015
            SAEConds.Add(makeSAECond(5, 5, 40, 40, 1, true));
            APIConds.Add(makeAPICond(APIclasses[9], 2, true));

            SAEConds[18].ConditionsSet = condSets[11];
            APIConds[11].ConditionsSet = condSets[11];

            //рено логан 2 2016
            SAEConds.Add(makeSAECond(0, 0, 40, 40, 1, true));
            SAEConds.Add(makeSAECond(5, 5, 50, 50, 2, true));
            APIConds.Add(makeAPICond(APIclasses[9], 3, true));

            SAEConds[19].ConditionsSet = condSets[12];
            SAEConds[20].ConditionsSet = condSets[12];
            APIConds[12].ConditionsSet = condSets[12];

            //рено дастер 2014
            SAEConds.Add(makeSAECond(5, 10, 40, 40, 1, true));
            APIConds.Add(makeAPICond(APIclasses[9], 2, true));

            SAEConds[21].ConditionsSet = condSets[13];
            APIConds[13].ConditionsSet = condSets[13];

            //рено дастер 2015
            SAEConds.Add(makeSAECond(5, 5, 40, 40, 1, true));
            APIConds.Add(makeAPICond(APIclasses[9], 2, true));

            SAEConds[22].ConditionsSet = condSets[14];
            APIConds[14].ConditionsSet = condSets[14];

            //рено дастер 2016
            SAEConds.Add(makeSAECond(0, 0, 40, 40, 1, true));
            SAEConds.Add(makeSAECond(5, 5, 50, 50, 2, true));
            APIConds.Add(makeAPICond(APIclasses[9], 3, true));

            SAEConds[23].ConditionsSet = condSets[15];
            SAEConds[24].ConditionsSet = condSets[15];
            APIConds[15].ConditionsSet = condSets[15];

            //ваз ларгус 2014
            SAEConds.Add(makeSAECond(5, 10, 40, 40, 1, true));
            SAEConds.Add(makeSAECond(10, 10, 30, 30, 2, true));
            APIConds.Add(makeAPICond(APIclasses[6], 3, true));

            SAEConds[25].ConditionsSet = condSets[16];
            SAEConds[26].ConditionsSet = condSets[16];
            APIConds[16].ConditionsSet = condSets[16];

            /*
            List<APIQualityClass> APIclasses = new List<APIQualityClass>();
            List<MotorOil> motorOils = initMotorOils(APIclasses);
            List<Company> companies = makeCompanies();
            List<Store> stores = new List<Store>();
            List<MotorOilMerch> merches = new List<MotorOilMerch>();

            List<SAEViscosityCondition> SAEConds = new List<SAEViscosityCondition>();
            List<APIQualityCondition> APIConds = new List<APIQualityCondition>();
            List<ConditionsSet> condSets = new List<ConditionsSet>();
            List<CarType> cars = new List<CarType>();

             */

            foreach (APIQualityClass el in APIclasses)
            {
                db.APIQualityClasses.Add(el);
            }
            foreach (MotorOil el in motorOils)
            {
                db.MotorOils.Add(el);
            }
            foreach (Company el in companies)
            {
                db.Companies.Add(el);
            }
            foreach (Store el in stores)
            {
                db.Stores.Add(el);
            }
            foreach (MotorOilMerch el in merches)
            {
                db.MotorOilMerches.Add(el);
            }
            foreach (SAEViscosityCondition el in SAEConds)
            {
                db.SAEViscosityConditions.Add(el);
            }
            foreach (ConditionsSet el in condSets)
            {
                db.ConditionsSets.Add(el);
            }
            foreach (CarType el in cars)
            {
                db.CarTypes.Add(el);
            }
            foreach (APIQualityCondition el in APIConds)
            {
                db.APIQualityConditions.Add(el);
            }
            db.SaveChanges();
        }
        //SetMotorOilData
        public MotorOil MakeMOD(string Name, int OnCold, int OnHot, APIQualityClass APIGas, int Volume, string Producer)
        {
            MotorOil motorOil = new MotorOil();
            motorOil.Name = Name;
            motorOil.SAEViscosity = new SAEViscosity();
            motorOil.SAEViscosity.OnCold = OnCold;
            motorOil.SAEViscosity.OnHot = OnHot;
            motorOil.APIQualityClass = APIGas;
            motorOil.Volume = Volume;
            motorOil.Producer = Producer;
            return motorOil;
        }
        public APIQualityClass MakeQC(string Name, int qualityValue)
        {
            APIQualityClass qc = new APIQualityClass();
            qc.QualityValue = qualityValue;
            qc.Name = Name;
            return qc;
        }
        public List<MotorOil> initMotorOils(List<APIQualityClass> APIclasses)
        {
            //5 rolf 5 takayama 6 lukoil 5 shell
            List<MotorOil> motorOils = new List<MotorOil>();
            //rolf
            motorOils.Add(MakeMOD("ROLF GT", 5, 30, APIclasses[9], 4, "ROLF"));
            motorOils.Add(MakeMOD("ROLF GT", 5, 40, APIclasses[11], 4, "ROLF"));
            motorOils.Add(MakeMOD("ROLF Energy", 10, 40, APIclasses[9], 4, "ROLF"));
            motorOils.Add(MakeMOD("ROLF Ultra", 0, 30, APIclasses[12], 1, "ROLF"));
            motorOils.Add(MakeMOD("ROLF KRAFTON P5 U", 10, 40, APIclasses[9], 4, "ROLF"));
            //takayama
            motorOils.Add(MakeMOD("TAKAYAMA adaptec", 5, 40, APIclasses[11], 4, "TAKAYAMA"));
            motorOils.Add(MakeMOD("TAKAYAMA safetec", 10, 40, APIclasses[9], 4, "TAKAYAMA"));
            motorOils.Add(MakeMOD("TAKAYAMA adaptec", 5, 40, APIclasses[11], 1, "TAKAYAMA"));
            motorOils.Add(MakeMOD("TAKAYAMA", 5, 30, APIclasses[9], 4, "TAKAYAMA"));
            motorOils.Add(MakeMOD("TAKAYAMA", 5, 30, APIclasses[11], 4, "TAKAYAMA"));

            //lukoil
            motorOils.Add(MakeMOD("LUKOIL GENESIS ARMORTECH JP", 5, 30, APIclasses[11], 4, "LUKOIL"));
            motorOils.Add(MakeMOD("LUKOIL GENESIS ARMORTECH DX1", 5, 30, APIclasses[12], 4, "LUKOIL"));
            motorOils.Add(MakeMOD("LUKOIL GENESIS ARMORTECH JP", 0, 30, APIclasses[12], 4, "LUKOIL"));
            motorOils.Add(MakeMOD("LUKOIL GENESIS UNIVERSAL", 10, 40, APIclasses[11], 4, "LUKOIL"));
            motorOils.Add(MakeMOD("LUKOIL LUXE", 5, 30, APIclasses[9], 4, "LUKOIL"));
            motorOils.Add(MakeMOD("LUKOIL GENESIS ARMORTECH GC", 5, 30, APIclasses[11], 1, "LUKOIL"));
            //shell
            motorOils.Add(MakeMOD("Shell Helix Ultra", 5, 30, APIclasses[9], 4, "SHELL"));
            motorOils.Add(MakeMOD("Shell HELIX HX8 SYNTHETIC", 5, 40, APIclasses[11], 4, "SHELL"));
            motorOils.Add(MakeMOD("Shell Helix Ultra", 5, 40, APIclasses[11], 4, "SHELL"));
            motorOils.Add(MakeMOD("Shell HELIX HX7", 10, 40, APIclasses[11], 4, "SHELL"));
            motorOils.Add(MakeMOD("Shell Helix Ultra", 5, 40, APIclasses[11], 1, "SHELL"));
            return motorOils;
        }

        public Company makeCompany(string Name)
        {
            Company company = new Company();
            company.Name = Name;
            return company;
        }

        public List<Company> makeCompanies()
        {
            List<Company> companies = new List<Company>();
            companies.Add(makeCompany("Рога и копыта"));
            companies.Add(makeCompany("Масла Челябинска"));
            companies.Add(makeCompany("Довольная машина"));
            return companies;
        }

        public Store makeStore(Company company, string Address)
        {
            Store store = new Store();
            store.Company = company;
            store.Adress = Address;
            return store;
        }

        public MotorOilMerch makeMerch(MotorOil oil, Store store, double Price, double StockCount)
        {
            MotorOilMerch merch = new MotorOilMerch();
            merch.MotorOil = oil;
            merch.Price = Price;
            merch.Store = store;
            merch.StockCount = StockCount;
            return merch;
        }

        public SAEViscosityCondition makeSAECond(int minCold, int maxCold, int minHot, int maxHot, int priority, bool isAllowing)
        {
            SAEViscosityCondition condition = new SAEViscosityCondition();
            condition.minHot = minHot;
            condition.maxHot = maxHot;
            condition.minCold = minCold;
            condition.maxCold = maxCold;
            condition.isAllowing = isAllowing;
            condition.priority = priority;
            return condition;
        }

        public APIQualityCondition makeAPICond(APIQualityClass minAPIClass, int priority, bool isAllowing)
        {
            APIQualityCondition condition = new APIQualityCondition();
            condition.priority = priority;
            condition.isAllowing = isAllowing;
            condition.MinAPIQualityClass = minAPIClass;
            return condition;
        }

        public CarType makeCar(string Name, ConditionsSet conds)
        {
            CarType car = new CarType();
            car.Name = Name;
            car.conditionsSet = conds;
            return car;
        }
    }
}
