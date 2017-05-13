using SISHaU.DataAccess.Definition;

namespace SISHaU.DataAccess.Model
{
    /// <summary>
    /// Эта сущность сделана для примера.
    /// Здесь ты можешь видеть, как элемент с данными принимает значения в конструкторе
    /// Так удобнее создавать новые элементы и потом сохранять их
    /// </summary>
    public class FileServiceLogEntity : EntityDto
    {
        private string _logData;
        public virtual string LogData {
            get => _logData;
            set => _logData = value;
        }
        public FileServiceLogEntity() { }

        public FileServiceLogEntity(string logData)
        {
            _logData = logData;
        }
    }

    /// <summary>
    /// Это маппинг данных
    /// По-умолчанию я устанавливаю колонку-идентификатор. Это генератор (GUID)
    /// </summary>
    public class FileServiceLogEntityMap : MapAction<FileServiceLogEntity>
    {
        public FileServiceLogEntityMap() : base("FileServiceLog", id => id.Id)
        {
            Map(m => m.LogData);
        }
    }
}
