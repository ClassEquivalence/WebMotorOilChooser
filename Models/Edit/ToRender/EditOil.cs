using WebApplication1.Models.MotorOilStats;

namespace WebApplication1.Models.Edit.ToRender
{
    public class EditOil
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

        public string? OilName { get; set; }
        public string? Producer {  get; set; }
        public APIQualityClass QualityClass { get; set; }
        public List<APIQualityClass> QualityClasses { get; set; }
        public decimal Volume { get; set; }
        public string imgPath { get; set; }
        public string editOilState { get; set; }
    }
}
