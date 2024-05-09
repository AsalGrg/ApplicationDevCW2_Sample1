import { Button, Text, TextInput, Textarea } from "@mantine/core";
import React, { useEffect, useRef, useState } from "react";
import { MdCancel } from "react-icons/md";
import { add_blog } from "../../services/addBlog";
import { useNavigate, useParams } from "react-router";
import { get_blog_details } from "../../services/getBlogDetails";
import { edit_blog } from "../../services/editBlog";


const AddBlog = () => {
  const [coverImage, setcoverImage] = useState(null);
  const [title, settitle] = useState('')
  const [description, setdescription] = useState('')

  const navigate = useNavigate();
  const {type, id}= useParams();

  useEffect(() => {
    async function getEditDetails(){
      if(type==="new")return;
      

      const res = await get_blog_details(id);

      if(res.ok){
        const data = await res.json();
      
        setcoverImage(data.CoverImage);
        settitle(data.Title)
        setdescription(data.Body)
      }
    }
    getEditDetails();
  }, [])
  

  const imgRef = useRef();

  async function addBlog(){

    var res;
    if(type==='new'){
      res= await add_blog(title,description,coverImage);
    }else if(type==='edit'){
      res= await edit_blog(title,description,coverImage, id);
    }

    if(res.ok){
        console.log("perfect")
        return navigate('/')
    }
  }

  
  return (
    <div className="d-flex justify-content-center w-100">
      <div
        className="w-50 mt-5 py-5 d-flex flex-column gap-4"
        style={{
          minHeight: "100vh",
        }}
      >
        <Text size="25px" fw={700} className="text-center">
          Add Blog
        </Text>

        <TextInput placeholder="Blog Title" label="Title"
        value={title}
        onChange={(e)=> settitle(e.target.value)}
        />

        <div
          className="border rounded border-2 w-100 position-relative"
          style={{
            height: "400px",
          }}
        >
          {coverImage ? (
            <>
              <img
                src={coverImage instanceof File? URL.createObjectURL(coverImage): coverImage}
                className="w-100 h-100"
                style={{
                  objectFit: "cover",
                }}
              ></img>
              <Button className="position-absolute top-0 start-100 translate-middle"
              variant="filled" color={'red'}
              size="sm"
              onClick={()=> setcoverImage(null)}
              >
                <MdCancel/>
              </Button>
            </>
          ) : (
            <div
              className="w-100 h-100 d-flex justify-content-center align-items-center"
              onClick={() => {
                imgRef.current.click();
              }}
            >
              <div
                className="rounded border bg-primary py-5 text-center"
                style={{
                  height: "130px",
                  width: "150px",
                }}
              >
                Select Cover Image
              </div>

              <input
                type="file"
                className="visually-hidden"
                ref={imgRef}
                onChange={(e) => setcoverImage(e.target.files[0])}
                value={coverImage}
              ></input>
            </div>
          )}
        </div>

        <Textarea placeholder="Blog description" autosize
        label="Blog description"
        minRows={5}
        value={description}
        onChange={(e)=> setdescription(e.target.value)}
        />

        <div className="w-100 d-flex justify-content-end"
        onClick={addBlog}
        >
            <Button>Save</Button>
        </div>
      </div>
    </div>
  );
};

export default AddBlog;
