namespace Game1
{
    public class Material
    {
        public float estatic;
        public float dynamic;

        public Material()
        {
            estatic = 0.1f;
            dynamic = 0;
        }

        public Material(float elastic,float dynamic)
        {
            this.estatic = elastic;
            this.dynamic = dynamic;
        }

    }
}