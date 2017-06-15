public class RenderUtils {

    private static short drawOrder = short.MinValue;

    public static short GetDrawOrder() {
        return drawOrder++;
    }

    public static void ClearDrawOrder() {
        drawOrder = short.MinValue;
    }

}
