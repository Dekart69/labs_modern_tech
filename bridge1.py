# 2. Патерн "Міст"
class Renderer:
    def render(self, shape):
        pass


class VectorRenderer(Renderer):
    def render(self, shape):
        print(f"Drawing {shape} as vector")


class RasterRenderer(Renderer):
    def render(self, shape):
        print(f"Drawing {shape} as raster")


class Shape:
    def __init__(self, renderer):
        self.renderer = renderer

    def draw(self):
        pass


class Circle(Shape):
    def draw(self):
        self.renderer.render("Circle")


circle1 = Circle(VectorRenderer())
circle1.draw()

circle2 = Circle(RasterRenderer())
circle2.draw()
