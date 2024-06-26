﻿using WebApplication1.Models.MotorOilStats;
using WebApplication1.Models.ToRender;

namespace WebApplication1.Models.Edit.ToRender
{
    public class EditOil : RenderBase
    {
        /*
                 <div>
            <h4>Название масла</h4>
            <input type="text" name="OilName" required>
        </div>
        <div>
            <h4>Производитель</h4>
            <input type="text" name="Producer" required>
        </div>
        <div>
            <h4>Класс качества</h4>
            <input type="radio" name="APIQualityClass">
            <input type="radio" name="APIQualityClass">
            <input type="radio" name="APIQualityClass">
        </div>
        <div>
            <h4>Объём канистры(для указания масла на розлив укажите 0 объём)</h4>
            <input type="number" name="Volume" required>
        </div>
        <div>
            <h4>Изображение</h4>
            <div id="imgEditContainer" hidden>
                <img id="img" draggable="false" style="top: 0px; left: 0px;">
            </div>
            <input type="number" id="imgScale" value="10" min="2" max="100" hidden>
            <input id="oilImgInput" type="file" name="Image">
        </div>
         */
        public int id { get; set; }
        public string? OilName { get; set; }
        public string? Producer {  get; set; }
        public APIQualityClass QualityClass { get; set; }
        public List<APIQualityClass> QualityClasses { get; set; }
        public SAEViscosity SAEViscosity {  get; set; }
        public decimal Volume { get; set; }
        public string imgPath { get; set; }
        public string editOilState { get; set; }

        public EditOil(ApplicationContext db, MotorOil oil, bool isEdit)
        {
            id = oil.id;
            OilName = oil.Name;
            Producer = oil.Producer;
            QualityClass = oil.APIQualityClass;
            QualityClasses = db.APIQualityClasses.ToList();
            SAEViscosity = oil.SAEViscosity;
            Volume = oil.Volume;
            imgPath = oil.GetImgNamePath();
            if (isEdit)
            {
                editOilState = "true";
            }
            else
            { editOilState = "false"; id = -1; }
        }
    }
}
