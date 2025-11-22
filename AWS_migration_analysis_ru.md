# AWS: анализ переноса контента из Einstein-Engines-RU в invicta-station

## Что переносить
- Код: `Content.Client/AWS/**`, `Content.Server/AWS/**`, `Content.Shared/AWS/**` (экономика/банк, страхование, зарплаты и Payday, cargo-bridge, криминальные антаги, продаваемые батареи, гравитация, навыки и история, UI и сообщения).
- Прототипы: `Resources/Prototypes/AWS/**` (экономика/банкоматы/терминалы/страховки/зарплаты, карты/ид-карты/радиоканалы, навыки/атрибуты/очки возраста, культура/фракции/образ жизни, спавнеры работ и отделов, карта `cyberiad`, кошельки и ключи).
- Локали: `Resources/Locale/en-US/aws/**`, `Resources/Locale/ru-RU/aws/**` плюс строки про законы в `Resources/Locale/*/station-laws`.
- Графика: `Resources/Textures/AWS/economy/*.rsi` (ATM, терминал, страховые иконки, кошелек, продаваемые батареи).
- Прочее: справочник `Resources/Prototypes/Guidebook/economy_insurance_icons.yml`, радиоканалы/группы идентификаторов `Resources/Prototypes/AWS/radio_channels.yml` и `name_identifier_groups.yml`.

## Зависимости AWS на внешние подсистемы
- Спавн игроков: `Content.Server/Station/Systems/StationSpawningSystem.cs` выдает банковский аккаунт ID/PDA через `EconomyAccountHolderComponent` и `EconomyBankAccountSystem`; также хранит префаб должности в `PresetIdCardComponent`. Нужны соответствующие компоненты на прототипах ID/PDA.
- Инвентарь HUD: `Content.Shared/Inventory/InventorySystem.Relay.cs` добавляет показ иконок страхования (`EconomyShowInsuranceIconsComponent`), поэтому без AWS-компонентов HUD не соберется.
- Профиль персонажа: `Content.Shared/Preferences/HumanoidCharacterProfile.cs` хранит выбранную страховку `EconomyInsurancePrototype`, значение по умолчанию `"NonStatus"`.
- Гравитация: `Content.Server/Gravity/GravityGeneratorSystem.cs` и `GravityGeneratorComponent.cs` шлют события в `AwsGravityGeneratorFieldSystem`; клиентское окно зарядки (`Content.Client/Power/PowerCharge/PowerChargeWindow.xaml.cs`) читает `AwsGravityGeneratorStatusComponent` для массы станции/FTL-статуса.
- Админ-вербы: `Content.Server/Administration/Systems/AdminVerbSystem.Antags.cs` содержит экшен для антага `CriminalAntag` (прототип должен быть портирован).
- Cargo-bridge: `Content.Server/AWS/Economy/CargoBridge/CargoSystem.AwsBridge.cs` и клиентские `Cargo*Aws*.cs` — partial-расширения существующих `CargoSystem`/UI, требуют совместимости с базовыми partial-классами.
- Экономика: `EconomyBankAccountSystem` использует `NameIdentifier`, доступы, вендинги, StationSystem, GameTicker, VendingMachineSystem, AccessReaderSystem и др.; зарплатные рулсеты (`PaydayRuleSystem`, `Salary*RuleSystem`) регистрируются как стандартные геймрулы.
- Навыки/история: `Content.Server/AWS/SkillSystem` и `Content.Client/AWS/Skills/*` используют мета-компоненты в `Content.Shared/AWS/Skills/**`; персонажи получают контейнер навыков через `CharacterSkillComponent` и прототипы навыков/категорий/атрибутов.

## Обратные ссылки на AWS вне папки AWS
- Прототипы вендингов: `Resources/Prototypes/DeltaV/.../vending_machines.yml`, `_Lavaland/.../vending_machines.yml`, `Nyanotrasen/.../vending_machines.yml` помечены `#AWS-edit` (изменены родители/ассортимент).
- Оружие: `Resources/Prototypes/DeltaV/Entities/Objects/Weapons/Guns/Rifles/rifles.yml` содержит `CAWS-25 Jackdaw`.
- Датасет законов: `Resources/Prototypes/Datasets/ion_storm.yml` добавляет строку `MORE LAWS`.
- Законы кремниевых: `Resources/Prototypes/silicon-laws.yml` и `DeltaV/siliconlaws.yml` — дополнения к законам.
- Путеводитель: `Resources/Prototypes/Guidebook/economy_insurance_icons.yml` использует `/Textures/AWS/economy/insurance.rsi`.
- Языковые трейты: `Resources/Prototypes/Traits/languages.yml` и `_EE/Traits/languages.yml` содержат закомментированные блоки `#AWS-start/#AWS-end` с ограничениями по национальностям.
- Локаль: `Resources/Locale/ru-RU/WWDP_TRANSLATION/_MainS/entity.ftl` содержит строки с `AWS`.
- Гравитационный UI: `Content.Client/Power/PowerCharge/PowerChargeWindow.xaml.cs` (упомянуто выше).
- Тесты: `Content.IntegrationTests/Tests/EntityTest.cs` импортирует `Content.Shared.AWS.Economy.Bank`.

## Риски и особые моменты
- Папка `Resources/Prototypes/AWS/Roles/Jobs/�orporations` имеет битую кодировку имени; при копировании убедиться, что FS сохранил название (скорее всего должно быть `Corporations`).
- Partial-классы AWS должны совпасть с сигнатурами базовых систем (CargoSystem, BoundUI). Изменения в invicta-station базовых файлах могут потребовать ручного мерджа.
- Значения по умолчанию (например, страховка `"NonStatus"`, префиксы счетов, группы идентификаторов) должны существовать в прототипах, иначе рантайм/профили упадут.
- Ассеты `/Textures/AWS/...` referenced напрямую из прототипов (в т.ч. вне папки AWS) — отсутствие приведет к ошибкам загрузки ресурсов.
- Экономические геймрулы (`EconomyPayDayRule`, `PaydayRule`, `Salary*Rule`) должны быть подключены к системе геймрулов; проверить конфиг и списки доступных правил в invicta-station.

## Черновой план переноса
1) Скопировать AWS-деревья (код/прототипы/локали/текстуры) из `Einstein-Engines-RU` в `invicta-station`, сохранить структуру путей.  
2) Применить патчи к внешним файлам: `StationSpawningSystem.cs`, `InventorySystem.Relay.cs`, `HumanoidCharacterProfile.cs`, `GravityGeneratorSystem.cs`, `GravityGeneratorComponent.cs`, `PowerChargeWindow.xaml.cs`, `AdminVerbSystem.Antags.cs`, а также к упомянутым YAML-прототипам (vending, rifles, ion_storm, silicon laws, guidebook, trait languages, локаль).  
3) Проверить совместимость partial-классов Cargo/BoundUI с текущими версиями invicta-station; при расхождениях перенести логику вручную.  
4) Убедиться, что прототипы ID/PDA и job спавнеров включают компоненты AWS (держатели банковских счетов, каналы связи, группы идентификаторов, страховка) и что значения по умолчанию совпадают с ожиданиями кода.  
5) Прогнать сборку + базовые игровые тесты (спавн персонажа, открытие ATM/терминала, HUD страховки, геймрулы Payday/Salary, cargo-заказы, гравитация) для ловли отсутствующих ресурсов и несовместимостей.
