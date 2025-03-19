# 1. Патерн "Адаптер"
class OldPrinter:
    def old_print(self, text):
        print(f"Old Printer: {text}")

class Adapter:
    def __init__(self, old_printer):
        self.old_printer = old_printer

    def print(self, text):
        self.old_printer.old_print(text)

printer = Adapter(OldPrinter())
printer.print("Hello, Adapter!")