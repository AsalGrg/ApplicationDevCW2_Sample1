import { Avatar, Text } from "@mantine/core";
import React from "react";
import { useNavigate } from "react-router";

const EachBlogCard = () => {
    const navigate = useNavigate();
  return (
    <section
      className="rounded ps-2 row justify-content-between cursor-pointer"

      onClick={()=> navigate('/blog/1')}
      style={{
        minHeight: "130px",
        maxHeight: "180px",
      }}
    >
      <div className="col-7 d-flex flex-column justify-content-center align-items-start gap-2">

        <div className="d-flex align-items-center gap-2">
          <Avatar size={"md"} />
          <Text size="sm" fw={"500"}>
            Asal Gurung
          </Text>
        </div>
        <Text size="xl" fw={"700"} lineClamp={2}>
          Blog Title
        </Text>

        <Text size="md" fw={"400"} lineClamp={2}>
          Lorem ipsum dolor, sit amet consectetur adipisicing elit. Nemo, autem
          dolor tempore est mollitia impedit, corrupti totam repellat omnis eum
          consequuntur ad voluptatibus culpa! Commodi voluptatum provident illo
          eveniet aperiam?
        </Text>

        <div className="mt-1">
          <Text size="sm" fw={"400"} lineClamp={2}>
            03 May, 2023
          </Text>
        </div>
      </div>

      <div className="col-4">
        <img
          src="https://images.pexels.com/photos/236047/pexels-photo-236047.jpeg?cs=srgb&dl=clouds-cloudy-countryside-236047.jpg&fm=jpg"
          className="w-100 img-fluid"
          style={{
            objectFit: "cover",
            height: "180px",
          }}
        />
      </div>
    </section>
  );
};

export default EachBlogCard;
