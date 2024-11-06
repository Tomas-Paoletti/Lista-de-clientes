# Lista-de-clientes

Esta aplicación de consola en C# utiliza Entity Framework para conectarse a una base de datos y listar clientes nacidos antes del año 2000 junto con sus compras. Las compras están ordenadas por fecha y total, y se muestra un total acumulado por cliente.


## Estructura del Proyecto

El proyecto tiene las siguientes clases principales en el archivo Progrma.cs:

* **Program** : La clase principal que contiene el método `Main` para inicializar la base de datos y mostrar el listado de compras de cada cliente.
* **DatabaseContext** : La clase que define el contexto de Entity Framework y las tablas de `Customers` (clientes) y `Purchases` (compras).
* **Customer** : La clase que representa a los clientes, con propiedades como `CustomerId`, `FullName` y `DateOfBirth`.
* **Purchase** : La clase que representa las compras, con propiedades como `PurchaseId`, `PurchaseDateUTC`, `Total` y `CustomerId`.


## Funcionalidad

### 1. Inicialización de la Base de Datos

La función `InitializeDB` en `Program` agrega datos de ejemplo a las tablas `Customers` y `Purchases` si están vacías. Estos datos incluyen clientes con su fecha de nacimiento y varias compras , cada una con una fecha y un total de compra.

### 2. Listado de Compras de Clientes

En `Main`, el código hace lo siguiente:

* **Filtra Clientes** : Selecciona solo aquellos nacidos antes del año 2000.
* **Obtiene las Compras** : Para cada cliente, obtiene todas las compras asociadas, ordenadas primero por fecha de compra (de más reciente a más antigua) y luego por el monto total (en caso de fechas iguales).
* **Cálculo de Edad** : Utiliza el método `GetAge` para calcular la edad exacta del cliente basado en su fecha de nacimiento.
* **Formato de Salida** : Para cada cliente, muestra su nombre, edad y una lista de compras. La salida está formateada con alineación y separación para mayor claridad:
* La fecha de compra se ajusta a la zona horaria GMT-3.
* Los montos de compra y el total acumulado están en formato de moneda.

### 3. Opción para Cerrar el Script

Después de mostrar el listado, el usuario tiene la opción de cerrar el script escribiendo "salir" en la consola.

## Ejemplo de Salida del Script:

```bash
Clientes nacidos antes del año 2000 y sus compras ordenadas por fecha
=====================================================================
Sanchez Mario (Edad: 39 años)
========================================
09/02/2021   ----   $        673
09/02/2021   ----   $        672
02/02/2021   ----   $        256
02/02/2021   ----   $        255
02/01/2021   ----   $      1.000
---------------------------------------
TOTAL:    $ 2.856
========================================

Gomez Ricardo (Edad: 30 años)
========================================
07/02/2021   ----   $        888
07/02/2021   ----   $        887
25/01/2021   ----   $         10
25/01/2021   ----   $        111
25/01/2021   ----   $          1
25/01/2021   ----   $          1
15/01/2021   ----   $     12.000
15/01/2021   ----   $     12.000
11/01/2021   ----   $        987
11/01/2021   ----   $        987
---------------------------------------
TOTAL:   $ 27.872
========================================

Escriba 'salir' para cerrar el script.
```


## Cómo Ejecutar

1. Asegúrate de tener .net Framework 4.6.1.
2. Asegúrate de tener Entity Framework instalado y configurado en tu proyecto.
3. Abre la solucion en visual studio que se encuentra dentro de la carpeta DotNetExam.
4. Compila y ejecuta el programa en un entorno de consola.
5. El listado de clientes y compras se mostrará en la consola.
6. Para finalizar el script, escribe "salir" en la consola.

## Notas

* **Zona horaria** : La fecha de compra se ajusta a la zona horaria GMT-3.
* **Formato de salida** : Se usa `String.Format` para alinear la salida en columnas.
* **Configuración regional** : La moneda y formato de fecha dependen de la configuración regional del sistema.
