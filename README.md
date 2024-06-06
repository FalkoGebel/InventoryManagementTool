# Inventory Management Tool

## Concept

### Basics
- Progamming Language: C#
- UI: WPF
- Database: SQL

### Use Cases
- Administrate products (Create, Edit, Inactivate)
- Administrate different areas within the warehouse (Create, Edit, Inactivate)
- Manipulate stock (positive adjustments, negative adjustments)

### Database Structure
- Table "Product" (Code, Description)
- Table "Area" (Code, Description)
- Table "Adjustment" (Product, Area, Date, Quantity)

![Database Design](/DatabaseDesign/erm_basic.jpg?raw=true "Inventory Management Tool")