﻿using Microsoft.EntityFrameworkCore;
using WebApplication1.Models.ChoiceHelpers;
using WebApplication1.Models.FormsData;

namespace WebApplication1.Models.ToRender
{
    public class ChoosebyCar: OilsAndFilters
    {
        public List<CarType> CarList;
        public override void PrepareData(ApplicationContext db)
        {
            base.PrepareData(db);
            CarList = db.CarTypes.Include(cs => cs.conditionsSet).ThenInclude(cond => cond.APIQualityConditions).ThenInclude(apiq=>apiq.MinAPIQualityClass)
                .Include(cs => cs.conditionsSet).ThenInclude(cond => cond.SAEViscosityConditions)
                .Include(cs => cs.conditionsSet).ThenInclude(cond => cond.OilTypeConditions).ThenInclude(mo=>mo.MotorOil).ToList();
        }
        public void PrepareData(ApplicationContext db, OilFiltersFormData filters, string carName)
        {
            PrepareData(db);
            CarType? car = null;
            foreach (CarType c in CarList)
            {
                if (c.Name == carName)
                {
                    car = c;
                    break;
                }
            }
            if (car == null)
            {
                base.PrepareData(db, filters);
                return;
            }
                //throw new Exception("Car not found(tried finding it by Name)");


            List<MotorOilMerch> newList = new List<MotorOilMerch>();
            MotorOil oil;
            foreach (MotorOilMerch merch in MotorOilMerches)
            {
                if (filters.OilSuitsFilter(merch) && car.conditionsSet.OilAcceptable(merch.MotorOil))
                    newList.Add(merch);
            }
            MotorOilMerches = newList;
        }
    }
}
