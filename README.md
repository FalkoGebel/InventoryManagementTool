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
- Table "Product"/"Item" (name, description)
- Table "Area" (name/code, description)
- Table "Adjustment" (date, item, area, quantity, positive or negative)