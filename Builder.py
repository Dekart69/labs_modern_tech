from abc import ABC, abstractmethod


# Абстрактний будівельник
class Builder(ABC):
    @abstractmethod
    def reset(self):
        pass

    @abstractmethod
    def set_part_a(self, value):
        pass

    @abstractmethod
    def set_part_b(self, value):
        pass

    @abstractmethod
    def set_part_c(self, value):
        pass


# Конкретний будівельник
class ConcreteBuilder(Builder):
    def __init__(self):
        self.reset()

    def reset(self):
        self._product = Product()

    def set_part_a(self, value):
        self._product.part_a = value

    def set_part_b(self, value):
        self._product.part_b = value

    def set_part_c(self, value):
        self._product.part_c = value

    def get_result(self):
        return self._product


# Продукт
class Product:
    def __init__(self):
        self.part_a = None
        self.part_b = None
        self.part_c = None

    def __str__(self):
        return f"Product with A: {self.part_a}, B: {self.part_b}, C: {self.part_c}"


# Директор (необов'язковий)
class Director:
    def __init__(self, builder: Builder):
        self._builder = builder

    def construct_minimal_viable_product(self):
        self._builder.reset()
        self._builder.set_part_a("Basic A")

    def construct_full_featured_product(self):
        self._builder.reset()
        self._builder.set_part_a("Advanced A")
        self._builder.set_part_b("Advanced B")
        self._builder.set_part_c("Advanced C")


# Використання
builder = ConcreteBuilder()
director = Director(builder)

director.construct_minimal_viable_product()
product1 = builder.get_result()
print(product1)

director.construct_full_featured_product()
product2 = builder.get_result()
print(product2)
