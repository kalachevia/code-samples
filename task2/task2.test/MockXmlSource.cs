 using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

using task2.Graph;

namespace task2.test {
  class MockXmlSource : IGraphSource<XDocument> {

    private string testXml = 
@"<?xml version=""1.0"" encoding=""utf-8""?>
<graph>
	<node id=""1"" role=""start"">
		<link ref=""2"" weight=""5"" />
	</node>
	<node id=""2"">
		<link ref=""1"" weight=""5"" />
		<link ref=""3"" weight=""5"" />
	</node>
    <node id=""3"" role=""finish"" >
        <link ref=""2"" weight=""5"" />
        <link ref=""4"" weight=""5"" />
    </node>
    <node id=""4"" status=""crash"">
        <link ref=""3"" weight=""5"" />
    </node>
</graph>";

    public XDocument GetData() {
      return XDocument.Parse(testXml);
    }
  }
}
