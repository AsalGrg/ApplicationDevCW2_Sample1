import React from 'react'
import Navbar from '../navbar/Navbar'
import { Outlet } from 'react-router'

const UserLayout = () => {
  return (
    <div>
        <Navbar/>

        <div className='mb-5'>
            <Outlet/>
        </div>
    </div>
  )
}

export default UserLayout