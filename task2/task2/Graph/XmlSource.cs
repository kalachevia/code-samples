using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using System.Xml.Schema;
using System.Xml;
using System.IO;

namespace task2.Graph {
  /// <summary>
  /// Reads and validates graph data from xml file
  /// </summary>
  class XmlSource : IGraphSource<XDocument> {

    public XmlSource(string path, string schema = "") {
      Path = path;
      Schema = schema;
    }

    public string Path { get; set; }
    public string Schema { get; set; }

    public XDocument GetData() {
      if (string.IsNullOrWhiteSpace(Path))
        throw new InvalidOperationException("File path is empty");

      if (!File.Exists(Path))
        throw new FileNotFoundException("Specified file was not found");
        //Read file and create XDocument
        var doc = XDocument.Parse(File.ReadAllText(Path));
       
        //Xml data validation
        if (!string.IsNullOrEmpty(Schema)) {
          var schemas = new XmlSchemaSet();
          schemas.Add("", XmlReader.Create(new StringReader(Schema)));
          doc.Validate(schemas, (obj, args) => {
            throw new XmlException("Invalid XML");
          });
        }

        return doc;
    }
  }
}
