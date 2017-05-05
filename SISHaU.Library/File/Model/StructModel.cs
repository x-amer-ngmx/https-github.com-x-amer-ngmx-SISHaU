using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SISHaU.Library.File.Model
{
    /// <summary>
    /// Не стандартные Http Заголовки для обработки запросов и ответов при Загрузке/Выгрузке файлов.
    /// </summary>
    public enum HeadParam
    {
        Date,
        Last_Modified,
        Location,
        Content_Length,
        Content_Type,
        Connection,

        /// <summary>
        /// Идентификатор поставщика данных
        /// </summary>
        X_Upload_Dataprovider,

        /// <summary>
        /// Определяет имя загружаемого файла. Допустимы следующие расширения:
        /// pdf,
        /// docx,
        /// doc,
        /// rtf,
        /// txt,
        /// xls,
        /// xlsx,
        /// jpeg,
        /// jpg,
        /// bmp,
        /// tif,
        /// tiff,
        /// gif,
        /// zip,
        /// rar,
        /// csv,
        /// odp,
        /// odf,
        /// ods,
        /// odt,
        /// sxc,
        /// sxw
        /// </summary>
        X_Upload_Filename,

        /// <summary>
        /// Определяет размер файла в байтах.
        /// </summary>
        X_Upload_Length,

        /// <summary>
        /// Количество частей, на которые разделен файл для передачи.
        /// </summary>
        X_Upload_Part_Count,

        /// <summary>
        /// Порядковый номер части файла.
        /// </summary>
        X_Upload_Partnumber,

        /// <summary>
        /// Идентификатор файла в системе ГИС ЖКХ (Ответ)
        /// </summary>
        X_Upload_UploadID,

        /// <summary>
        /// GUID загрузаемого файла
        /// </summary>
        X_Upload_FileGUID,

        /// <summary>
        /// 
        /// </summary>
        X_Upload_Completed,


        /// <summary>
        /// Части загружаемого файла
        /// </summary>
        X_Upload_Completed_Parts,

        /// <summary>
        /// Ошибка возникшая при загрузке файла (Ответ)
        /// </summary>
        X_Upload_Error
    }

    /// <summary>
    /// Oпределяет хранилище, в которое должен быть загружен файл
    /// </summary>
    public enum Repo
    {
        /// <summary>
        /// Подсистема Управление домами, Лицевые счета
        /// </summary>
        Homemanagement,

        /// <summary>
        /// Подсистема Реестр коммунальной инфраструктуры
        /// </summary>
        Rki,

        /// <summary>
        /// Подсистема Голосования
        /// </summary>
        Voting,

        /// <summary>
        /// Подсистема Инспектирование жилищного фонда
        /// </summary>
        Inspection,

        /// <summary>
        /// Подсистема Оповещения
        /// </summary>
        Informing,

        /// <summary>
        /// Подсистема Электронные счета
        /// </summary>
        Bills,

        /// <summary>
        /// Подсистема Лицензии
        /// </summary>
        Licenses,

        /// <summary>
        /// Подсистема Договора (ДУ, уставы, ДПОИ)
        /// </summary>
        Agreements,

        /// <summary>
        /// Подсистема Нормативно-справочная информации
        /// </summary>
        Nsi,

        /// <summary>
        /// Подсистема Раскрытие деятельности УО
        /// </summary>
        Disclosure
    }
}
