import { Text } from "@mantine/core";
import React from "react";

const BlogBody = () => {
  return (
    <section className="mt-5 d-flex flex-column gap-4">
      <div className="d-flex justify-content-center">
        <img
          src="https://miro.medium.com/v2/resize:fit:828/format:webp/1*897tOXFHq3TtOM3mpaZ3vQ.jpeg"
          alt=""
          style={{
            height: "400px",
            objectFit: "cover",
          }}
          className=""
        />
      </div>

      <Text size="md" fw={400}>
        Lorem ipsum dolor sit amet consectetur adipisicing elit. Illo sapiente
        vero consequuntur cumque totam fugit. Sequi laborum quam animi inventore
        voluptates. Iusto similique quisquam praesentium totam, beatae enim,
        tenetur hic, error blanditiis repudiandae accusamus animi consectetur
        itaque amet labore aperiam distinctio atque necessitatibus esse magni
        sit molestiae pariatur. Rem illum quisquam harum libero molestias labore
        amet praesentium, quod a adipisci velit omnis reiciendis incidunt
        repudiandae minima odit ducimus perferendis quibusdam laborum soluta
        fuga quasi nesciunt saepe. Animi aspernatur eligendi cum quas,
        reprehenderit sunt eius saepe deserunt iure officia iusto eveniet,
        tenetur nulla eaque placeat culpa aliquam explicabo veniam harum!
        Tempora. Lorem ipsum dolor, sit amet consectetur adipisicing elit.
        Veniam corporis excepturi fugiat iste veritatis est eum, dignissimos,
        dolorem ea impedit reprehenderit vel.
        <br />
        Odio inventore quo modi, illo officiis itaque nemo possimus asperiores
        ea repudiandae cumque expedita ut incidunt, tempora dolorum dolore.
        Delectus inventore recusandae tempora accusantium quae atque odit
        dolores fugiat quia, laboriosam pariatur ducimus qui alias molestiae
        molestias totam dicta quos ratione voluptatum eligendi, sint facere est
        optio dignissimos. Fugiat aliquam optio officia magnam expedita
        voluptatum, quia earum facilis, id ex dolorum corrupti accusamus velit
        consequuntur harum autem natus, incidunt dicta totam nulla assumenda
        voluptatibus! Dolorum aliquam repellendus similique.
      </Text>
    </section>
  );
};

export default BlogBody;
