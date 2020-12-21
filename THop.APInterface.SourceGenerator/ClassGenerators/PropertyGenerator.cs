namespace THop.APInterface.SourceGenerator.ClassGenerators
{
    public class PropertyGenerator 
    {

        public AccessibilityType Accessibility { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public bool Static { get; set; }

        public bool GetSet { get; set; }

    }
}