# 3. Патерн "Відвідувач"
class Visitor:
    def visit(self, element):
        pass

class HtmlVisitor(Visitor):
    def visit(self, element):
        print(f"Converting {element.__class__.__name__} to HTML")

class MarkdownVisitor(Visitor):
    def visit(self, element):
        print(f"Converting {element.__class__.__name__} to Markdown")

class Element:
    def accept(self, visitor):
        visitor.visit(self)

class Text(Element):
    pass

text = Text()
text.accept(HtmlVisitor())
text.accept(MarkdownVisitor())
