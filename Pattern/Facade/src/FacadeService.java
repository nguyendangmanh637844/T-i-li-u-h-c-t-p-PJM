import services.GiaoHangService;
import services.GuiHangService;
import services.KiNhanService;

public class FacadeService {
    private final static FacadeService instance = new FacadeService();
    private GiaoHangService giaoHangService;
    private GuiHangService guiHangService;
    private KiNhanService kiNhanService;

    private FacadeService() {
        giaoHangService = new GiaoHangService();
        guiHangService = new GuiHangService();
        kiNhanService = new KiNhanService();
    }

    public static FacadeService getInstance() {
        return instance;
    }

    public void giaoHangNhanh() {
        guiHangService.guiHang();
        giaoHangService.giaoHangBangOTo();
        kiNhanService.kiNhan();
    }

    public void giaoHangBinhThuong() {
        guiHangService.guiHang();
        giaoHangService.giaoHangBangXeMay();
        kiNhanService.kiNhan();
    }
}
