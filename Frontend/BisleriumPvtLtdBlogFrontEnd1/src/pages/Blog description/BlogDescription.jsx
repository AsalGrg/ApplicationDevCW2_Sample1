import React from 'react'
import BlogIntroduction from '../../components/Blog description/BlogIntroduction'
import BlogBody from '../../components/Blog description/BlogBody'
import CommentsSection from '../../components/Blog description/CommentsSection'

const BlogDescription = () => {

  return (
    <div className='d-flex justify-content-center w-100'>
        <div className='w-75'
        style={{
            minHeight: '100vh'
        }}
        >
           <BlogIntroduction/> 
           <BlogBody/>
           <CommentsSection/>
        </div>
    </div>
  )
}

export default BlogDescription