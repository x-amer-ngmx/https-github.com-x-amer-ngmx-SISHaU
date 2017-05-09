
namespace SISHaU.Library.File.Model
{
    /// <summary>
    /// Перечень возвращаемых типов ошибок при загрузке/выгрузке
    /// </summary>
    public enum XErrorContext
    {
        /// <summary>
        /// Не пройдены проверки на корректность заполнения полей (обязательность, формат и т.п.).
        /// </summary>
        FieldValidationException,

        /// <summary>
        /// Некорректный размер файла
        /// </summary>
        InvalidSizeException,

        /// <summary>
        /// Не пройдены проверки на соответствие контрольной сумме
        /// </summary>
        HashConflictException,

        /// <summary>
        /// Неправильное хранилище
        /// </summary>
        ContextNotFoundException,

        /// <summary>
        /// Не удалось определить тип загружаемого файла или тип файла является недопустимым
        /// </summary>
        DetectionException,

        /// <summary>
        /// Поставщик данных не найден, заблокирован или неактивен.
        /// </summary>
        DataProviderValidationException,

        /// <summary>
        /// Информационная система не найдена по отпечатку или заблокирована.
        /// </summary>
        CertificateValidationException,

        /// <summary>
        /// Не пройдены проверки на существование идентификатора сессии.
        /// </summary>
        FileNotFoundException,

        /// <summary>
        /// Не пройдены проверки на соответствие операции многопоточной загрузки текущей сессии (например файл уже финализирован).
        /// </summary>
        InvalidStatusException,

        /// <summary>
        /// Не пройдены проверки на номер части (номер превышает количество частей, указанных в инициализации).
        /// </summary>
        InvalidPartNumberException,

        /// <summary>
        /// При нарушении ограничений безопасности при работе с файловым хранилищем (например, сессия загрузки инициализирована другим поставщиком данных).
        /// </summary>
        FilePermissionException,

        /// <summary>
        /// Содержимое файла инфицировано
        /// </summary>
        FileVirusInfectionException,

        /// <summary>
        /// Проверка на вредоносное содержимое не выполнялась, выполняется или дата завершения проверки меньше даты обновления антивирусных баз данных.
        /// </summary>
        FileVirusNotCheckedException,

    }

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
        /// Отпечаток сертификата в случает не шифрованного соединения
        /// </summary>
        X_Client_Cert_Fingerprint,

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
