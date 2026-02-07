CREATE TABLE users (
    id SERIAL PRIMARY KEY,
    name VARCHAR(50)  NOT NULL,
    email VARCHAR(100) UNIQUE NOT NULL,
    password_hash VARCHAR(255) NOT NULL,
    created_at TIMESTAMP DEFAULT NOW()
);
CREATE TABLE carts (
    id SERIAL PRIMARY KEY,
    user_id INT UNIQUE REFERENCES users(id) ON DELETE CASCADE,
    created_at TIMESTAMP DEFAULT NOW()
);

CREATE TABLE products (
    id SERIAL PRIMARY KEY,
    title VARCHAR(255) NOT NULL,
    description TEXT,
    price NUMERIC(10,2) NOT NULL,
    image_url VARCHAR(255),
    created_at TIMESTAMP DEFAULT NOW()
);
ALTER TABLE products
ADD COLUMN gender VARCHAR(255);

ALTER TABLE products
ADD COLUMN category VARCHAR(255);

CREATE TABLE cart_items (
    id SERIAL PRIMARY KEY,
    cart_id INT REFERENCES carts(id) ON DELETE CASCADE,
    product_id INT REFERENCES products(id),
    quantity INT DEFAULT 1
);

CREATE TABLE orders (
    id SERIAL PRIMARY KEY,
    user_id INT REFERENCES users(id),
    total NUMERIC(10,2) DEFAULT 0,
    status VARCHAR(50) DEFAULT 'pending',
    created_at TIMESTAMP DEFAULT NOW()
);

CREATE TABLE order_items (
    id SERIAL PRIMARY KEY,
    order_id INT REFERENCES orders(id) ON DELETE CASCADE,
    product_id INT REFERENCES products(id),
    quantity INT DEFAULT 1,
    price NUMERIC(10,2) NOT NULL
);

CREATE INDEX idx_cart_user ON carts(user_id);
CREATE INDEX idx_cartitems_cart ON cart_items(cart_id);
CREATE INDEX idx_order_user ON orders(user_id);
CREATE INDEX idx_orderitems_order ON order_items(order_id);

ALTER TABLE users
RENAME COLUMN password_hash TO passwordhash;
