public class PresupuestoViewModel
{
    private List<Cliente> clientes;

    public PresupuestoViewModel(List<Cliente> clientes)
    {
        this.clientes = clientes;
    }

    public List<Cliente> Clientes { get => clientes; }
}