Actúa como un desarrollador de software trabajando en un proyecto de **Ingeniería de Aplicaciones**. Estamos desarrollando un juego donde cada entidad (incluidos los enemigos y elementos interactivos) tiene su propio movimiento y comportamiento asignado, siguiendo una arquitectura basada en **Presentador y Vista** (donde cada entidad posee su presentador y una vista que lo dibuja).

### Reglas estrictas de ejecución
1. **Respetar el código actual:** NO modifiques lógica, comportamientos, movimientos, contratos existentes ni archivos que no estén directamente involucrados en las asignaciones solicitadas, salvo que sea estrictamente necesario para conectar el sprite en la Vista o el Presentador correspondiente.
2. **Trabajar paso a paso:** Antes de editar o crear archivos, inspeccioná el proyecto, ubicá las carpetas de sprites, las entidades, sus presentadores y sus vistas, y listá qué archivos vas a tocar y qué sprite asignarás a cada uno.
3. **Sin suposiciones destructivas:** Si un nombre de clase, archivo, vista o presentador varía ligeramente respecto a los nombres indicados abajo, identificá la correspondencia exacta en el código actual y confirmala en tu plan antes de aplicar cambios.

### Asignaciones de sprites requeridas

Desde `ActRPGsprites/sp`:
- **`sp_85`** -> Asignar como sprite para la entidad **`Explosion`** (en su vista / renderizado correspondiente).
- **`sp_81`** -> Asignar como sprite para la entidad **`Moneda_Cobre`** (en su vista / renderizado correspondiente).

Desde `ActRPGsprites/en`:
- **`en_152`** -> Asignar como sprite para **`enemigo_basico`**.
- **`en_106`** -> Asignar como sprite para **`enemigo_evasivo`**.
- **`en_372`** -> Asignar como sprite para **`enemigo_avaro`**.
- **`en_88`** -> Asignar como sprite para **`enemigo_tanque`**.
- **`en_194`** -> Asignar como sprite para **`enemigo_fantasma`**.

### Pasos a seguir
1. Buscá y verificá la existencia y ruta exacta de cada sprite (`sp_85`, `sp_81`, `en_152`, `en_106`, `en_372`, `en_88`, `en_194`) en `ActRPGsprites/sp` y `ActRPGsprites/en`.
2. Localizá la Vista (y su Presentador asociado si corresponde) de cada entidad: `Explosion`, `Moneda_Cobre`, `enemigo_basico`, `enemigo_evasivo`, `enemigo_avaro`, `enemigo_tanque` y `enemigo_fantasma`.
3. Mostrá el mapeo encontrado y esperá mi confirmación o aplicá los cambios de forma incremental manteniendo intacto el resto del comportamiento del juego.
4. Al finalizar cada cambio, resumí qué archivo se modificó y qué quedó sin tocar.