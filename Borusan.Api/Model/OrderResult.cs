namespace Borusan.Api.Model
{
	public class OrderResult
	{
		public string? MusteriSiparisNo { get; set; }
		public int SistemSiparisNo { get; set; }
		public bool Statu { get; set; }
		public string? HataAciklama { get; set; }
	}
}
