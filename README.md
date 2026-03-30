# Simulerade bin och virtuella blommor
**En teknisk prototyp för ekosystem-simulation i Unity Engine.**

Det här projektet startade som en utforskning av det komplexa samspelet mellan bin och deras flora. Genom att kombinera mekaniker från flygsimulatorer med strategiska element från RTS-genren, skildras en värld där pollinatörer och växter lever i en ömsesidig beroendeställning. Den långsiktiga visionen är att skapa en modell för hur evolution faktiskt ser ut i praktiken; där blommor kan korsbefruktas och spelaren genom selektion kan styra ängens utveckling över tid.

## 🎮 Upplevelse och mekanik
I simulationen växlar du sömlöst mellan att styra ett enskilt bi i tredjepersonsperspektiv och att inta en strategisk översiktsroll för att hantera hela kolonin. Fokus ligger på resurshantering där insamling av nektar, pollen och propolis är direkt kopplat till kupans expansion. 

De autonoma arbetsbina agerar utifrån de instruktioner de får, medan floran de interagerar med följer egna livscykler. Växterna sås naturligt i närheten av sina artfränder och genomgår faser från tillväxt till blomning och slutligen vissnande, vilket skapar en ständigt föränderlig spelplan som kräver strategisk anpassning.

## 🛠 Tekniska vägval
Eftersom målet var att skapa en bas för framtida utbyggnad, lades stor vikt vid en modulär arkitektur enligt *Single Responsibility Principle*. För att simuleringen ska kunna skalas upp till stora svärmar utan att prestandan dyker, implementerades ett skräddarsytt system för målsökning. Istället för att förlita mig på kostsam Raycasting använder jag ett listbaserat system för binas navigation, vilket visat sig vara betydligt mer effektivt för beräkningarna.

Renderingen optimerades ytterligare genom att använda Level-of-Detail (LOD) på bi-modellerna och lågpolymodeller skapade i Blender. Själva logiken för både binas beteenden (från vila till resursinsamling) och blommornas livsfaser styrs av tillståndsmaskiner (FSM), vilket håller koden ren och förutsägbar.

## 📈 Lärdomar och vägen framåt
Arbetet med prototypen har varit en djupdykning i praktisk spelutveckling. Det har inneburit allt från att trimma vektormatematik för att få till en naturlig acceleration i flykten, till att strukturera komplexa UI-system med flera paneler i Unity. 

Framöver ligger fokus på att öka den biologiska komplexiteten. Det innefattar allt från ett klassystem för drönare (scouter, vakter och krigare) till mer dynamiska miljöfaktorer som dygnsvariationer och naturliga fiender som getingar. Genom att introducera korspollinering hoppas jag kunna simulera den enorma mångfald som finns i naturen, där spelarens agerande direkt påverkar den genetiska sammansättningen på ängen.

---
**Teknisk stack:** Unity Engine & C# (Visual Studio), Blender för modellering, Git för versionshantering.

*Dokumentationen är framtagen i samarbete med språkmodellen Gemini, med projektets tekniska rapport som underlag.*
