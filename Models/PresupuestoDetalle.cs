    public class PresupuestoDetalle
    {
        private Producto producto;
        private int cantidad;

        public Producto Producto { get => producto; }
        public int Cantidad { get => cantidad; set => cantidad = value; }

        public void asignarProd(int id)
        {
            var prodRep = new ProductosRepository();
            producto = prodRep.GetProducto(id);
        }

        public void asignarProd(Producto prod)
        {
            producto = prod;
        }
    }